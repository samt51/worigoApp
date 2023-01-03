using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using Worigo.Core.Dtos.ResponseDtos;
using Worigo.Core.Exceptions;

namespace Worigo.API.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var exceptionfeatur = context.Features.Get<IExceptionHandlerFeature>();

                    var statuscode = exceptionfeatur.Error switch
                    {
                        ClientSideException =>400,
                        NotFoundException=>404,
                        AuthorizationException =>401,
                        DataControllException =>350,
                        _ =>500
                    };
                    context.Response.StatusCode = statuscode;
                    var errorlist = new List<string>();
                    errorlist.Add(exceptionfeatur.Error.Message);
                    var response = ResponseDto<NoContentResult>.Fail(statuscode, errorlist); //ErrorDto.Fail(exceptionfeatur.Error.Message,400,MessageEnum.IsFailed);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
