using Microsoft.EntityFrameworkCore;
using Npgsql;
using Sd.Crm.Backend.Common;
using Sd.Crm.Backend.DataLayer;
using Sd.Crm.Backend.DataLayer.Configuration;
using Serilog;
using Serilog.Events;

namespace Sd.Crm.Backend.Startup
{
    public static class StartupExtensions
    {

            public static IHostBuilder UseLogging(this IHostBuilder builder)
        {
            return builder.UseSerilog((ctx, lc) =>
            {
                lc
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.AspNetCore.HttpLogging", LogEventLevel.Information)
                    .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
                    .MinimumLevel.Override("Npgsql", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
#if DEBUG
            .MinimumLevel.Debug()
            .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
#else
                        .MinimumLevel.Information()
                        .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
#endif
                    ;
                if (!ctx.Configuration.IsProduction())
                {
                    lc.WriteTo.File("./logs/log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 2, shared: true, restrictedToMinimumLevel: LogEventLevel.Information);
                }
            });
        }

        public static bool IsProduction(this IConfiguration Configuration)
            => Configuration.GetValue<string>(EnvironmentVariables.ENVIRONMENT) == "production";

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<CrmContext>(options =>
            {
                if (!configuration.IsProduction())
                {
                    options.EnableSensitiveDataLogging();
                }

                var connsectionString = configuration.GetDatabaseConnectionString() ?? throw new ArgumentNullException(EnvironmentVariables.Database.DATABASE_NAME);
                var cb = new NpgsqlConnectionStringBuilder(connsectionString)
                {
                    Pooling = true,
                    MinPoolSize = 2,
                    MaxPoolSize = 50
                };
                options
                    .UseNpgsql(cb.ToString(), npg =>
                    {
                        npg.EnableRetryOnFailure();
                    })
                    .UseSnakeCaseNamingConvention();
            });
        }
    }
}
