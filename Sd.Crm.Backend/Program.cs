using Sd.Crm.Backend.Services;
using Sd.Crm.Backend.Startup;

namespace Sd.Crm.Backend
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {

            return await RunService(args);
        }

        private static async Task<int> RunService(string[] args)
        {
            var host = CreateHostBuilder(args)
            .Build();
            await host.RunAsync();
            return 0;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: false);
                    config.AddEnvironmentVariables("DATABASE_");
                    config.AddEnvironmentVariables("REDIS_");
                })
                .UseLogging()
                .ConfigureServices((context, services) => services.ConfigureServices(context.Configuration))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup.Startup>();
                })
                .UseConsoleLifetime();
    }
}
