using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaximaTech.Infrastructure.Data;
using MaximaTech.Infrastructure.Seeds;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MaximaTech.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;

                DatabaseMigration.Run(services);
                DatabaseSeed.Run(services);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseUrls("http://localhost:5000/")
                        .UseStartup<Startup>();
                });
    }
}
