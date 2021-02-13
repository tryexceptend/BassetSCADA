using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore;

namespace BassetS.WApi.Logger
{
    public class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                Console.Title = "BassetS.WApi.Logger";
                string configFileName = Environment.GetEnvironmentVariable("BASSETSCONFIG");
                if (configFileName == null || configFileName == "" ){
                    Environment.Exit(-1);
                }
                var config = new ConfigurationBuilder().AddJsonFile(configFileName)
                                                       .Build();
                
                CreateHostBuilder(args, config).Build().Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhandled exception: "+ex.Message);
                Environment.Exit(-1);
            }
        }
        /// <summary>
        /// Настройка хоста
        /// </summary>
        /// <param name="args">Аргументы</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns></returns>
        public static IWebHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(configuration)
                .UseStartup<Startup>()
                .ConfigureKestrel(kestrelOptions =>
                {
                    kestrelOptions.Limits.MaxConcurrentConnections = 10;
                    kestrelOptions.Listen(IPAddress.Loopback, int.Parse(configuration["nodeLogging:hosting:httpPort"]));
                });
    }
}
