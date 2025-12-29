using System;
using System.ComponentModel.DataAnnotations;
using FluentResults;
using JobService.Domain.ValueObjects;
using MediatR;

namespace JobService.Application.Features.JobPost.Command
{
    public class CreateJobPostCommand : IRequest<Result<string>>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Requirements { get; set; }

        public int BenefitId { get; set; }

        [Required]
        public string Location { get; set; }

        public SalaryRange? Salary { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime? ExpiresAt { get; set; }

        public int? JobId { get; set; }

        public string Source { get; set; }
        public string? ExternalJobBoardId { get; set; }
        public string SyncStatus { get; set; }
        public DateTime? LastSyncDate { get; set; }
        public ICollection<int> CompanyJobPreferenceIds { get; set; } = new List<int>();
        public IEnumerable<int> SkillIds { get; set; } = new List<int>();
        public int MinimumExperience { get; set; }
        public int? MinimumEducationLevelId { get; set; }
        public string MinimumEducationLevelDegree { get; set; }
        public string MinimumEducationLevelInstitution { get; set; }
        public string MinimumEducationLevelField { get; set; }
        public string MinimumEducationLevelDescription { get; set; }

        [Required]
        public int CityId { get; set; }

        public int? JobCategoryId { get; set; }
        public int? ProvinceId { get; set; }
        public int CompanyId { get; set; }
    }
}

