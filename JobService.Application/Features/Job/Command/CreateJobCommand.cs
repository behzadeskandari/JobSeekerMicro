using System.ComponentModel.DataAnnotations;
using FluentResults;
using JobSeeker.Shared.Contracts.Enums;
using MediatR;

namespace JobService.Application.Features.Job.Command
{
    public class CreateJobCommand : IRequest<Result<string>>
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public JobLevel Level { get; set; }

        [Required]
        public int CompanyId { get; set; }

        public bool IsProirity { get; set; }
        public JobType JobType { get; set; }
        public string? JobDescription { get; set; }
        public string? JobRequirment { get; set; }
        public int? CityId { get; set; }
        public int? TechnicalOptionsId { get; set; }
        [Required]
        public int JobCategoryId { get; set; }
        public JobOfferStatus Status { get; set; }
    }
}

