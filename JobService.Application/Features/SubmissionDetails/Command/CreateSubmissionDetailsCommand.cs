using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.SubmissionDetails.Command
{
    public class CreateSubmissionDetailsCommand : IRequest<Result<int>>
    {
        [Required]
        public int ApplicationId { get; set; }

        public string Source { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public string UserAgent { get; set; } = string.Empty;
    }
}

