using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(option =>
                    {

                        //opton.Limits.MaxConcurrentConnections = 100;

                        //opton.Limits.MaxConcurrentUpgradedConnections = 100;

                        // option.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(20);
                        //  opton.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(1);
                    });
                });
        //.ConfigureLogging(logging =>
        //        {
        //    logging.ClearProviders();
        //    logging.AddNLog("XmlConfig/NLog.config");
        //}).UseNLog();
    }
}
