using FluentResults;
using MediatR;

namespace JobService.Application.Features.CompanyJobPreferences.Command
{
    public class UpdateCompanyJobPreferencesCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string? PreferredSkills { get; set; }
        public string? PreferredEducationLevel { get; set; }
        public string? PreferredExperienceLevel { get; set; }
    }
}

