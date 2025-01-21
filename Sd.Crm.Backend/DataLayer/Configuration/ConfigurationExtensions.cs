using Sd.Crm.Backend.Common;

namespace Sd.Crm.Backend.DataLayer.Configuration
{
    public static class ConfigurationExtensions
    {
        public static string? GetDatabaseConnectionString(this IConfiguration Configuration)
        {
            var host = Configuration.GetValue<string>(EnvironmentVariables.Database.DATABASE_HOST) ?? Configuration.GetValue<string>("SQLServer:Host");
            var database = Configuration.GetValue<string>(EnvironmentVariables.Database.DATABASE_NAME) ?? Configuration.GetValue<string>("SQLServer:Name");
            var username = Configuration.GetValue<string>(EnvironmentVariables.Database.DATABASE_LOGIN) ?? Configuration.GetValue<string>("SQLServer:Login");
            var password = Configuration.GetValue<string>(EnvironmentVariables.Database.DATABASE_PASSWORD) ?? Configuration.GetValue<string>("SQLServer:Password");
            if (host == null || database == null || username == null)
            {
                return null;
            }
            var conn = $"Server={host};initial catalog={database};User Id={username};Password={password};Encrypt=false";
            return conn;
        }
    }
}
