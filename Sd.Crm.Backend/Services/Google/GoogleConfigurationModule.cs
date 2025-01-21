namespace Sd.Crm.Backend.Services.Google
{
    public static class GoogleConfigurationModule
    {
        public static void AddGoogleService(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IGoogleAccessService, GoogleAccessService>();
            services.Configure<GoogleOptions>(config.GetSection("google"));
        }
    }
}
