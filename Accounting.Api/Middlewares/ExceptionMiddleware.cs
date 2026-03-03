using Accounting.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Accounting.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            { 
                await _next(context);
            }
            catch(ValidationException ex)
            {
                await Handle(context, 400, ex.Message);
            }
            catch (BusinessException ex)
            {
                await Handle(context, 400, ex.Message);
            }
            catch (NotFoundException ex)
            {
                await Handle(context, 404, ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                await Handle(context, 401, ex.Message);
            }
            catch (Exception)
            {
                await Handle(context, 500, "Internal Server Error");
            }
        }

        private static async Task Handle(HttpContext context, int code, string message)
        {
            context.Response.StatusCode = code;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                status = code,
                error = message
            });
        }
    }
}
