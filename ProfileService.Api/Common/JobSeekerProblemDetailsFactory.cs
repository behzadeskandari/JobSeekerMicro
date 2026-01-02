using System.Diagnostics;
using ProfileService.Application.Interfaces;
using ProfileService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ProfileService.Api.Common
{
    public class JobSeekerProblemDetailsFactory : ProblemDetailsFactory
    {
        //private readonly IProfileServiceUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JobSeekerProblemDetailsFactory(
            //IProfileServiceUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            //_unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
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

            // Log to database
            //_ = Task.Run(async () =>
            //{
            //    try
            //    {
            //        var log = new Log
            //        {
            //            StatusCode = statusCode,
            //            Title = title,
            //            Type = type,
            //            Detail = detail,
            //            Instance = instance,
            //            TraceId = traceId,
            //            HttpContextUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name,
            //            HttpContextRequest = $"{httpContext.Request.Method} {httpContext.Request.Path}",
            //            HttpContextResponse = statusCode.ToString(),
            //            DateCreated = DateTime.UtcNow,
            //            IsActive = true
            //        };

            //        await _unitOfWork.Logs.AddAsync(log);
            //        await _unitOfWork.CommitAsync();
            //    }
            //    catch
            //    {
            //        // Silently fail logging to prevent circular errors
            //    }
            //});

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

            // Log to database
            //_ = Task.Run(async () =>
            //{
            //    try
            //    {
            //        var log = new Log
            //        {
            //            StatusCode = statusCode,
            //            Title = title ?? "Validation Error",
            //            Type = type,
            //            Detail = detail ?? string.Join(", ", modelStateDictionary.SelectMany(x => x.Value?.Errors ?? Enumerable.Empty<ModelError>()).Select(e => e.ErrorMessage)),
            //            Instance = instance,
            //            TraceId = traceId,
            //            HttpContextUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name,
            //            HttpContextRequest = $"{httpContext.Request.Method} {httpContext.Request.Path}",
            //            HttpContextResponse = statusCode.ToString(),
            //            DateCreated = DateTime.UtcNow,
            //            IsActive = true
            //        };

            //        await _unitOfWork.Logs.AddAsync(log);
            //        await _unitOfWork.CommitAsync();
            //    }
            //    catch
            //    {
            //        // Silently fail logging
            //    }
            //});

            return validationProblemDetails;
        }
    }
}

