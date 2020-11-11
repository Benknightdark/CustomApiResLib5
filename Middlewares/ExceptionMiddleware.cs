using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CutomApiLib.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace CutomApiLib.Middlewares {
    public static class ExceptionMiddleware {
        public static void UseCustomExceptionMiddleware (this IApplicationBuilder app) {
            app.UseExceptionHandler (errorApp => {
                errorApp.Run (async context => {

                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature> ();
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = new MediaTypeHeaderValue ("application/json").ToString ();
                    string ErrorContent = exceptionHandlerPathFeature?.Error?.Message + " => " + exceptionHandlerPathFeature?.Error.StackTrace;
                    var bodyContent = new CustomResponseModel {
                        status = 500,
                        title = context.Response.StatusCode.ToString (),
                        errors = ErrorContent
                    };
                    await context.Response.WriteAsync (JsonSerializer.Serialize (bodyContent), Encoding.UTF8);
                });
            });
        }
    }
}