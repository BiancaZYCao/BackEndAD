using Arch.EntityFrameworkCore.UnitOfWork;
using BackEndAD.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MS.UnitOfWork
{
    /// <summary>
    ///在 <see cref="IServiceCollection"/>中安装工作单元依赖注入的扩展方法
    /// </summary>
    public static class UnitOfWorkServiceExtensions
    {
        /// <summary>
        /// 在<see cref ="IServiceCollection"/>中注册给定上下文作为服务的工作单元。
        /// 同时注册了dbcontext
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <remarks>此方法仅支持一个db上下文，如果多次调用，将抛出异常。</remarks>
        /// <returns></returns>
        public static IServiceCollection AddUnitOfWorkService<TContext>(this IServiceCollection services, System.Action<DbContextOptionsBuilder> action) where TContext : DbContext
        {
            //register dbcontext
            services.AddDbContext<TContext>(action);
            //register unitofwork
            
            // Following has a issue: IUnitOfWork cannot support multiple dbcontext/database, 
            // that means cannot call AddUnitOfWork<TContext> multiple times.
            // Solution: check IUnitOfWork whether or null
            //services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();
            return services;
        }
    }
}