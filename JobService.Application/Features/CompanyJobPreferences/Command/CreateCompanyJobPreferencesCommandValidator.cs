using FluentValidation;

namespace JobService.Application.Features.CompanyJobPreferences.Command
{
    public class CreateCompanyJobPreferencesCommandValidator : AbstractValidator<CreateCompanyJobPreferencesCommand>
    {
        public CreateCompanyJobPreferencesCommandValidator()
        {
            RuleFor(x => x.JobPostId).GreaterThan(0).WithMessage("JobPostId must be greater than 0");
        }
    }
}

