using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BackEndAD.Models;
using BackEndAD.DataContext;
using BackEndAD.Repo;
using BackEndAD.ServiceInterface;
using BackEndAD.ServiceImpl;
using Hangfire;
using Hangfire.MemoryStorage;
using BackEndAD.Controllers;

namespace BackEndAD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*services.AddDbContext<DbContext>(opt =>
               opt.UseInMemoryDatabase("TodoList"));*/
            //services.AddDbContext<ProjectContext>(opt =>
            //opt.UseSqlServer(Configuration.GetConnectionString("DbConn")));
            /*services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            services.AddScoped<IDepartmentRepo, DeparmentRepo>();
            services.AddScoped<IInventoryRepo, InventoryRepo>();
            services.AddScoped<ISupplierRepo, SupplierRepo>();
            services.AddScoped<IEmployeeService, EmployeeServiceImpl>();
            services.AddScoped<IDepartmentService, DepartmentServiceImpl>();
            
            services.AddScoped<ISupplierService, SupplierServiceImpl>();*/
            services.AddUnitOfWorkService<ProjectContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DbConn")); });
            //options.UseInMemoryDatabase("Try"); });
            services.AddScoped<IDepartmentService, DepartmentServiceImpl>();
            services.AddScoped<IStoreClerkService, StoreClerkServiceImpl>();
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi",
                    builder => builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            });

            services.AddHangfire(config =>
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseDefaultTypeSerializer()
            .UseMemoryStorage()

            );

            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env, 
            ProjectContext _context,
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsApi");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //_context.Database.EnsureDeleted();
            //_context.Database.EnsureCreated();
            //new Seeder(_context);
            //DBSeed.Initialize(IUnitOfWork<ProjectContext>);

            SchedulerController scheduler = new SchedulerController();
               
            app.UseHangfireDashboard();
            backgroundJobClient.Enqueue(() => scheduler.seeder());
            recurringJobManager.AddOrUpdate("compile reorder",() => scheduler.reorder(), "*/5 * * * *");
            recurringJobManager.AddOrUpdate("compile reorder monthly", () => scheduler.reorder(), "5 0 1 * *");
        }
    }
}
