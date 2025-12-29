using System;
using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobApplication.Command
{
    public class CreateJobApplicationCommand : IRequest<Result<string>>
    {
        [Required]
        public int JobId { get; set; }

        [Required]
        public int JobPostId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string ResumeFileName { get; set; }

        [Required]
        public string ResumeFileUrl { get; set; }

        public string CoverLetter { get; set; }

        public string Status { get; set; } = "Pending";

        public string Notes { get; set; }
    }
}

