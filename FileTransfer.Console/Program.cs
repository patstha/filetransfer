﻿using FileTransfer.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace FileTransfer.Console
{
    static class Program
    {
        static void Main(string[] args)
        {
            var host = AppStartup();

            var dataService = host.Services.GetRequiredService<IDataService>();

            dataService.Connect();
            var persons = dataService.GetPersons();
            var dapperPersons = dataService.GetPersonsDapper();
        }

        static void ConfigSetup(IConfigurationBuilder builder)
        {
            builder.SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();
        }

        static IHost AppStartup()
        {
            var builder = new ConfigurationBuilder();
            ConfigSetup(builder);

            // defining Serilog configs
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(@"mylog.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            // Initiated the dependency injection container 
            var host = Host.CreateDefaultBuilder()
                        .ConfigureServices((context, services) =>
                        {
                            services.AddTransient<IDataService, DataService>((services) =>
                            {
                                return new DataService(
                                    services.GetRequiredService<ILogger<DataService>>(),
                                    services
                                        .GetRequiredService<IConfiguration>()
                                        .GetConnectionString("Default"));
                            });
                        })
                        .UseSerilog()
                        .Build();

            return host;
        }
    }
}
