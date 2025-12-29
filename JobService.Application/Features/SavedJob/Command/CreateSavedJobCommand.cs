using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.SavedJob.Command
{
    public class CreateSavedJobCommand : IRequest<Result<int>>
    {
        [Required]
        public int JobPostId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
    }
}

