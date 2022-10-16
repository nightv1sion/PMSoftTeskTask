using Entities.ErrorModel;
using Entities.Exceptions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var errorDetails = new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        };

                        switch (contextFeature.Error)
                        {
                            case BadRequestException: errorDetails.StatusCode = StatusCodes.Status400BadRequest; 
                                context.Response.StatusCode = StatusCodes.Status400BadRequest; break;
                            case ConflictException: errorDetails.StatusCode = StatusCodes.Status409Conflict;
                                context.Response.StatusCode = StatusCodes.Status409Conflict; break;
                            default: context.Response.StatusCode = 500; break;
                        }
                        await context.Response.WriteAsync(errorDetails.ToString());
                    }
                });
            });
        }
    }
}
