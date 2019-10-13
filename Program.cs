using System.Linq;
using Iot.Device.DHTxx;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using pitempd;
using Prometheus;

namespace BugzapperLabs.Temperatured
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
                    webBuilder.UseUrls("http://*:80");
                })
                .UseSystemd()
                .ConfigureServices((hostContext, services) =>
                {
                    DhtBase sensor = null;
                    if (!args.Contains("--fake"))
                    {
                        sensor = new Dht22(2);
                    }

                    services.AddSingleton(x => new MetricServer(1234));
                    services.AddSingleton(x => new TemperatureSensor(sensor));
                    services.AddSingleton<TemperatureService>();
                    services.AddHostedService<TemperatureServiceWorker>();
                    services.AddHostedService<TemperatureWorker>();
                });
    }
}
