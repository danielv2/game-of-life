using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GOF.Host
{
    /// <summary>
    /// Main class of the application
    /// </summary>
    public class Program
    {
        /// <summary>
        ///  Get the port from the environment variable
        /// </summary>
        protected static string Port
        {
            get { return Environment.GetEnvironmentVariable("PORT", EnvironmentVariableTarget.Process); }
        }

        /// <summary>
        /// Main method of the application
        /// </summary>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }

        /// <summary>
        /// Create the host builder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
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