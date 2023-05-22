using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

            //ekledik
            //configuration.development.json
            //ve
            //configuration.production.json
            //dosyalarý için
            .ConfigureAppConfiguration((hostingContext,config) =>
            {
                config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName.ToLower()}.json")
                .AddEnvironmentVariables();
            })



                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
