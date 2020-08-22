using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BackEndAD.DataContext;
using BackEndAD.Repo;

namespace BackEndAD
{
    public class Program
    {

        public static void Main(string[] args)
        {
            try
            {
                /*
                 //This is for DB connection testing 
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                     builder.DataSource = "team8-sa50.database.windows.net";
                     builder.UserID = "Bianca";
                     builder.Password = "!Str0ngPsword";
                     builder.InitialCatalog = "ADProj";


                 using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                 {
                     Console.WriteLine("\nDB connection testing:");                
                     connection.Open();
                     Console.WriteLine("DB connection created.");
                     Console.WriteLine("=========================================\n");
                 }
                 */
                
                var host = CreateHostBuilder(args).Build();
                /*using (IServiceScope scope = host.Services.CreateScope())
                {
                    DBSeed.Initialize(scope.ServiceProvider.GetRequiredService<IUnitOfWork<ProjectContext>>());
                }*/
                host.Run();
            }    
             
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("\nPls check DB connection and try again. Press enter.");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}