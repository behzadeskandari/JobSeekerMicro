using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.CompanyJobPreferences.Command
{
    public class CreateCompanyJobPreferencesCommand : IRequest<Result<int>>
    {
        [Required]
        public int JobPostId { get; set; }

        public string PreferredSkills { get; set; } = string.Empty;
        public string PreferredEducationLevel { get; set; } = string.Empty;
        public string PreferredExperienceLevel { get; set; } = string.Empty;
    }
}

