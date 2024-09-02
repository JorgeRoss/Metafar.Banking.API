using Metafar.Banking.Application.UseCases.Common.Exceptions;
using Metafar.Banking.Cross.Common;
using System.Net;
using System.Text.Json;

namespace Metafar.Banking.API.Modules.GlobalException
{
    public class GlobalExceptionHandler : IMiddleware
    {
        private ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationExceptionCustom ex)
            {
                context.Response.ContentType = "application/json";
                await JsonSerializer.SerializeAsync(context.Response.Body,
                    new Response<object> { Message = "Validation Error", Errors = ex.Errors });
            }
            catch (UnauthorizedAccessException ex)
            {
                string message = ex.Message.ToString();
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                _logger.LogError($"Exception Details: {message}");

                var response = new Response<object>()
                {
                    Message = message
                };

                await JsonSerializer.SerializeAsync(context.Response.Body, response);
            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                _logger.LogError($"Exception Details: {message}");

                var response = new Response<object>()
                {
                    Message = message
                };

                await JsonSerializer.SerializeAsync(context.Response.Body, response);
            }
        }
    }
}
