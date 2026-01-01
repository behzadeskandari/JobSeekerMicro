using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AdvertisementService.Api.Common
{
    public class JobSeekerProblemDetailsFactory : ProblemDetailsFactory
    {
        public JobSeekerProblemDetailsFactory()
        {
        }

        public override ProblemDetails CreateProblemDetails(
            HttpContext httpContext,
            int? statusCode = null,
            string? title = null,
            string? type = null,
            string? detail = null,
            string? instance = null)
        {
            statusCode ??= 500;
            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Type = type,
                Detail = detail,
                Instance = instance,
                Extensions = { ["traceId"] = traceId }
            };

            return problemDetails;
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(
            HttpContext httpContext,
            ModelStateDictionary modelStateDictionary,
            int? statusCode = null,
            string? title = null,
            string? type = null,
            string? detail = null,
            string? instance = null)
        {
            statusCode ??= 400;
            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

            var validationProblemDetails = new ValidationProblemDetails(modelStateDictionary)
            {
                Status = statusCode,
                Title = title ?? "One or more validation errors occurred.",
                Type = type,
                Detail = detail,
                Instance = instance,
                Extensions = { ["traceId"] = traceId }
            };

            return validationProblemDetails;
        }
    }
}

