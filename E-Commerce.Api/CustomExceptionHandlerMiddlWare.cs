using Domain.Exceptions;
using Shared.ErorrModel;

namespace E_Commerce.Api
{
    public class CustomExceptionHandlerMiddlWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomExceptionHandlerMiddlWare(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddlWare>  logger)
        {
            _next = Next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await _next.Invoke(httpcontext);
                await HandleNotFoundEndPointAsync(httpcontext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleException(httpcontext, ex);
            }
        }

        private static async Task HandleException(HttpContext httpcontext, Exception ex)
        {
            var BadResponse = new ErrorToReturn
            {
                ErrorMessage = ex.Message,
            };
            httpcontext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                BadRequsteException badRequsteException=> GetBadRequestExcepion(badRequsteException , BadResponse),
                _ => StatusCodes.Status500InternalServerError,
            };

            //httpcontext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpcontext.Response.ContentType = "application/json";

            var Response = new ErrorToReturn
            {
                StatusCode = httpcontext.Response.StatusCode,
                ErrorMessage = ex.Message,
            };

            await httpcontext.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadRequestExcepion(BadRequsteException badRequsteException, ErrorToReturn response)
        {
            response.StatusCode = StatusCodes.Status400BadRequest;
            response.Errors = badRequsteException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpcontext)
        {
            if (httpcontext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var errorResponse = new ErrorToReturn
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {httpcontext.Request.Path} not found.",
                };
                await httpcontext.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
