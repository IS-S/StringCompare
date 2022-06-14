using SomeProject.Web.Application;
using SomeProject.Web.Application.Middlewares.ExceptionHandling;
using SomeProject.Web.Application.Settings;
using SomeProject.Web.Application.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tinkoff.Hosting;
using Tinkoff.Metrics;

namespace SomeProject.Web
{
    public class Startup
    {
        private const string ApplicationPath = "/";

        public Startup(IWebHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .ConfigureDynamicSettings()
                .Build();
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services.AddTinkoffDefaults();

            services.AddControllers();

            services.AddSwagger(ApplicationPath);
        }

        public void Configure(IApplicationBuilder global, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            global.MapDefaultsOnInternalPort();

            global.MapOnPublicPort(app =>
            {
                app.UseExceptionLogging(loggerFactory);

                app.UsePathBase(ApplicationPath);

                app.UseSwagger(ApplicationPath);

                app.UseDefaultMetrics();

                app.UseRouting();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            });
        }
    }
}
