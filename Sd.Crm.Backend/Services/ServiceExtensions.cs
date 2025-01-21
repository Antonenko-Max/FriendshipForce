using Microsoft.AspNetCore.Identity;
using Sd.Crm.Backend.Services.Google;
using Sd.Crm.Backend.Services.Internal;
using Sd.Crm.Backend.Services.Lead;
using Sd.Crm.Backend.Services.Squad;
using Sd.Crm.Backend.Services.User;
using Sd.Crm.Backend.Startup;

namespace Sd.Crm.Backend.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGoogleService(configuration);
            services.AddScoped<ISquadService, SquadService>();
            services.AddScoped<IInternalService, InternalService>();
            services.ConfigureDatabase(configuration);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<PasswordHasher<Model.UserModels.User>>();
            services.AddScoped<ILeadService, LeadService>();
            services.AddSingleton<MappingService>();
        }
    }
}
