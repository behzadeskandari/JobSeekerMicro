using System.Diagnostics;
using System.Net;
using System.Text.Json;
using AssessmentService.Application.Interfaces;
using AssessmentService.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AssessmentService.Api.Middleware
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

        public async Task InvokeAsync(HttpContext context, IAssessmentServiceUnitOfWork unitOfWork)
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

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, IAssessmentServiceUnitOfWork unitOfWork)
        {
            var traceId = Activity.Current?.Id ?? context.TraceIdentifier;
            var statusCode = HttpStatusCode.InternalServerError;
            var title = "An error occurred while processing your request";
            var detail = exception.Message;

            // Determine status code based on exception type
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

            // Log exception to database
            _ = Task.Run(async () =>
            {
                try
                {
                    var stackTrace = exception.StackTrace ?? string.Empty;
                    var className = string.Empty;
                    var methodName = string.Empty;

                    if (!string.IsNullOrEmpty(stackTrace))
                    {
                        var firstLine = stackTrace.Split('\n').FirstOrDefault();
                        if (!string.IsNullOrEmpty(firstLine))
                        {
                            // Try to extract class and method from stack trace
                            var parts = firstLine.Split(new[] { " at ", " in " }, StringSplitOptions.RemoveEmptyEntries);
                            if (parts.Length > 0)
                            {
                                var methodInfo = parts[0].Trim();
                                var lastDot = methodInfo.LastIndexOf('.');
                                if (lastDot > 0)
                                {
                                    className = methodInfo.Substring(0, lastDot);
                                    methodName = methodInfo.Substring(lastDot + 1);
                                }
                            }
                        }
                    }

                    var exceptionLog = new ExceptionLog
                    {
                        Message = exception.Message,
                        StackTrace = exception.StackTrace,
                        Source = exception.Source,
                        ExceptionType = exception.GetType().FullName,
                        ClassName = className,
                        MethodName = methodName,
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
                catch
                {
                    // Silently fail to prevent circular errors
                }
            });

            // Create response
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

