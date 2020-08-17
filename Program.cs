using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackEndAD
{
    public class Program
    {
        /*public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });*/
        static void Main(string[] args)
        {
            //this is testing DBconnection
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "team8-sa50.database.windows.net";
                builder.UserID = "Bianca";
                builder.Password = "!Str0ngPsword";
                builder.InitialCatalog = "ADProj";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nDB connection testing Query data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();
                    //StringBuilder sb = new StringBuilder();
                    //sb.Append("SELECT Name,Secret FROM TodoItems");
                    //String sql = sb.ToString();

                    //using (SqlCommand command = new SqlCommand(sql, connection))
                    //{
                    //    using (SqlDataReader reader = command.ExecuteReader())
                    //    {
                    //        while (reader.Read())
                    //        {
                    //            Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                    //        }
                    //    }
                    //}
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("\nPls check DB connection and try again. Press enter.");
                Console.ReadLine();
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        
    }
}