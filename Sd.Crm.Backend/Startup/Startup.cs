using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Sd.Crm.Backend.Authorization;
using Sd.Crm.Backend.Common;
using Sd.Crm.Backend.DataLayer;
using Sd.Crm.Backend.Misc.Swagger;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace Sd.Crm.Backend.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var version = Configuration.GetValue(EnvironmentVariables.APP_VERSION, $"{Assembly.GetExecutingAssembly().GetName().Version}");

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

            services.AddSingleton<IAuthorizationHandler, CrmAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, CrmPermissionPolicyProvider>();

            services.AddHealthChecks();

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerExamplesFromAssemblyOf<SdExample>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = $"{CommonConstants.ServiceName} {version}, dotnet {Environment.Version}, PC={Environment.ProcessorCount}, ServerGC={System.Runtime.GCSettings.IsServerGC}", Version = "v1" });
                c.ExampleFilters();
            });

            services.AddCors(o =>
                o.AddPolicy("SPA", pb =>
                    pb.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                )
            );

            services.AddHttpClient();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider provider, ILogger<Startup> logger)
        {
#if DEBUG
            app.UseDeveloperExceptionPage();
#else
            app.UseDeveloperExceptionPage();
#endif
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/healthz", new HealthCheckOptions() { Predicate = _ => _.Tags.Contains("live") });
                endpoints.MapHealthChecks("/ready", new HealthCheckOptions() { Predicate = _ => _.Tags.Contains("ready") });
                endpoints.MapControllers();
            });
#if DEBUG

            app.UseSpa(s => s.UseProxyToSpaDevelopmentServer("http://localhost:3000"));
#else

            var basePath = "/";
            var httpScheme = "http";
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpScheme}://{httpReq.Host.Value}{basePath}" } };
                });
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Sd Services Api V1");
            });
#endif
        }
    }
}