using FluentValidation;
using JobSeeker.Shared.Common.Errors;
using JobSeeker.Shared.Kernel.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JobService.Api.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var problemDetails = new ProblemDetails
            {
                Instance = context.HttpContext.Request.Path,
                Extensions = { ["traceId"] = System.Diagnostics.Activity.Current?.Id ?? context.HttpContext.TraceIdentifier }
            };

            switch (exception)
            {
                case ValidationException validationException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Validation Error";
                    problemDetails.Detail = string.Join(", ", validationException.Errors.Select(e => e.ErrorMessage));
                    break;

                case NotFoundError :
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Title = "Not Found";
                    problemDetails.Detail = exception.Message;
                    break;

                case ConflictError:
                    problemDetails.Status = StatusCodes.Status409Conflict;
                    problemDetails.Title = "Conflict";
                    problemDetails.Detail = exception.Message;
                    break;

                case UnauthorizedError:
                    problemDetails.Status = StatusCodes.Status403Forbidden;
                    problemDetails.Title = "Unauthorized";
                    problemDetails.Detail = exception.Message;
                    break;

                case DomainException domainException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Domain Error";
                    problemDetails.Detail = domainException.Message;
                    break;

                default:
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Title = "An error occurred while processing your request";
                    problemDetails.Detail = exception.Message;
                    break;
            }

            context.Result = new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status
            };

            context.ExceptionHandled = true;
        }
    }
}

