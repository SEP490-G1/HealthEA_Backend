using D_APP_API.Middlewares;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Middlewares
{
    /// <summary>
    /// Middleware for catch exception
    /// </summary>
    public class GlobalExceptionHandlingMiddleware : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            var response = exception switch
            {
                ApiException apiEx => new ProblemDetails
                {
                    Status = (int)apiEx.StatusCode,
                    Type = apiEx.GetType().Name,
                    Title = apiEx.Message,
                    Detail = apiEx.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                },
                _ => new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Type = exception.Message,
                    Title = "Internal server error",
                    Detail = "Internal server error",
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                }
            };

            await httpContext.Response.WriteAsJsonAsync(response);

            return true;
        }
    }
}
