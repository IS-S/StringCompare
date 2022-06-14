using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Tinkoff.HealthChecks;
using Tinkoff.Logging;
using Tinkoff.Metrics;
using Tinkoff.Metrics.AspNetCore;

namespace SomeProject.Web.Application;

public static class RegistrationExtensions
{
    public static void AddTinkoffDefaults(this IServiceCollection services)
    {
        services.AddSageLogging();

        services.AddMetrics();
        services.AddDefaultHealthChecks();
    }

    public static void MapDefaultsOnInternalPort(this IApplicationBuilder app) =>
        app.UseRouting().UseEndpoints(endpoints => endpoints.MapDefaultInternalEndpoints());

    private static void MapDefaultInternalEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapDefaultHealthChecks();
        endpoints.MapDefaultMetrics();
    }
}
