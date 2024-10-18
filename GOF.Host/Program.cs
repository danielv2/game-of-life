using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GOF.Host
{
    public class Program
    {
        protected static string Port
        {
            get { return Environment.GetEnvironmentVariable("PORT", EnvironmentVariableTarget.Process); }
        }
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;

                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                          .AddEnvironmentVariables(); // This will override the previous settings
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel();
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.ListenAnyIP(string.IsNullOrEmpty(Port) ? 5000 : Convert.ToInt32(Port));
                    });
                });
    }
}