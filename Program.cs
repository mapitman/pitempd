using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using pitempd;

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
                .UseSystemd()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<TemperatureSensor>();
                    services.AddSingleton<TemperatureService>();
                    services.AddHostedService<Worker>();
                });
    }
}
