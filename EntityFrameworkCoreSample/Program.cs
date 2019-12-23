using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkCoreSample.Data;
using EntityFrameworkCoreSample.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace EntityFrameworkCoreSample
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        //public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
        public static readonly string AppName = "EntityFrameworkCoreSample";

        public static void Main(string[] args)
        {
            //var configuration = GetConfiguration();

            //Log.Logger = CreateSerilogLogger(configuration);

            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...", AppName);
                var host = CreateWebHostBuilder(args).Build();

                Log.Information("Applying migrations ({ApplicationContext})...", AppName);
                host
                    .MigrateDbContext<MvcMovieContext>((context, services) =>
                    {
                        new MvcMovieContextSeed()
                          .SeedAsync(context)
                          .Wait();
                    });

                Log.Information("Starting web host ({ApplicationContext})...", AppName);
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
               WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>();
    }
}
