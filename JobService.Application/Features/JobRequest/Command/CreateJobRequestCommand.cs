using System.ComponentModel.DataAnnotations;
using FluentResults;
using JobSeeker.Shared.Contracts.Enums;
using MediatR;

namespace JobService.Application.Features.JobRequest.Command
{
    public class CreateJobRequestCommand : IRequest<Result<int>>
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int JobPostId { get; set; }

        [Required]
        public string CoverLetter { get; set; } = string.Empty;

        [Required]
        public string ResumeUrl { get; set; } = string.Empty;

        public JobRequestStatus Status { get; set; } = JobRequestStatus.Start;
    }
}

