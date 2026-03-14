using Serilog;
using System.Net;

namespace DocumentMe.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error("                                                                                                                ");
                Log.Error("****************************************************************************************************************");
                Log.Error("                                                                                                                ");
                Log.Error("################################################################################################################");
                Log.Error("                                                                                                                ");
                Log.Error(ex, "Balasubramanya - Unhandled exception occurred", ex.Message, ex.Source);
                Log.Error("                                                                                                                ");
                Log.Error("################################################################################################################");
                Log.Error("                                                                                                                ");
                Log.Error("****************************************************************************************************************");
                Log.Error("                                                                                                                ");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    message = "Internal Server Error"
                });
            }
        }
    }
}
