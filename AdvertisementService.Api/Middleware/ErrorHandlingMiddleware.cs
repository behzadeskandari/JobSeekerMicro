using System.Diagnostics;
using System.Net;
using System.Text.Json;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AdvertisementService.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IAdvertisementUnitOfWork unitOfWork)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex, unitOfWork);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, IAdvertisementUnitOfWork unitOfWork)
        {
            var traceId = Activity.Current?.Id ?? context.TraceIdentifier;
            var statusCode = HttpStatusCode.InternalServerError;
            var title = "An error occurred while processing your request";
            var detail = exception.Message;

            switch (exception)
            {
                case ArgumentNullException:
                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    title = "Invalid Request";
                    break;
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    title = "Unauthorized";
                    break;
                case KeyNotFoundException:
                case FileNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    title = "Not Found";
                    break;
            }

            _ = Task.Run(async () =>
            {
                try
                {
                    var exceptionLog = new ExceptionLog
                    {
                        Message = exception.Message,
                        StackTrace = exception.StackTrace,
                        ExceptionType = exception.GetType().FullName,
                        TraceId = traceId,
                        HttpContextUser = context.User?.Identity?.Name,
                        HttpContextRequest = $"{context.Request.Method} {context.Request.Path}",
                        InnerException = exception.InnerException?.Message,
                        DateCreated = DateTime.UtcNow,
                        IsActive = true
                    };

                    await unitOfWork.ExceptionLogs.AddAsync(exceptionLog);
                    await unitOfWork.CommitAsync();
                }
                catch { }
            });

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            var problemDetails = new
            {
                status = (int)statusCode,
                title = title,
                detail = detail,
                traceId = traceId,
                instance = context.Request.Path
            };

            var jsonResponse = JsonSerializer.Serialize(problemDetails);
            await response.WriteAsync(jsonResponse);
        }
    }
}

