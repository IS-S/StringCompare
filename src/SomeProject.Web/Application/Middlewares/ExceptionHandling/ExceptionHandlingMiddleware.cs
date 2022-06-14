using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace SomeProject.Web.Application.Middlewares.ExceptionHandling
{
    public static class ExceptionHandlingMiddleware
    {
        public static void UseExceptionLogging(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger(nameof(ExceptionHandlingMiddleware));

            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (OperationCanceledException ex)
                {
                    logger.LogWarning(ex, "OperationCanceled");
                    context.Response.StatusCode = 444; // Connection closed without response
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "ServerError");
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
            });
        }
    }
}
