using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace SomeProject.Web.Application.Swagger;

public static class SwaggerExtensions
{
    public static void AddSwagger(this IServiceCollection services, string applicationPath)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddServer(new OpenApiServer { Description = "SomeProject Description", Url = applicationPath });

            options.SwaggerDoc("v1", new OpenApiInfo { Title = "SomeProject", Version = "v1" });

            string documentFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, documentFileName));
        });
    }

    public static void UseSwagger(this IApplicationBuilder app, string applicationPath)
    {
        app.UseSwagger(o => { o.RouteTemplate = "swagger/{documentName}/swagger.json"; });
        app.UseSwaggerUI(o => { o.SwaggerEndpoint(applicationPath + "/swagger/v1/swagger.json", "SomeProject"); });
    }
}
