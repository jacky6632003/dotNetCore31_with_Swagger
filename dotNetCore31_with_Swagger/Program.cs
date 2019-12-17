using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace dotNetCore31_with_Swagger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureWebHostDefaults(webBuilder =>
                       {
                           // Uses DOTNET_ environment variables and command line args

                           webBuilder.UseStartup<Startup>();
                       })
                       .ConfigureAppConfiguration((hostingContext, config) =>
                       {
                           // JSON files, User secrets, environment variables and command line arguments

                           NLogBuilder.ConfigureNLog("NLog.config");
                       })
                       .ConfigureLogging(logging =>
                       {
                           // Adds loggers for console, debug, event source, and EventLog (Windows only)

                           logging.ClearProviders();
                           logging.AddConsole();
                           logging.SetMinimumLevel(LogLevel.Trace);
                       })
                       .UseDefaultServiceProvider((context, options) =>
                       {
                           // Configures DI provider validation

                           options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                           options.ValidateOnBuild = true;
                       })
                       .UseNLog();
        }
    }
}