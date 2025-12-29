using FluentValidation;

namespace JobService.Application.Features.CompanyJobPreferences.Command
{
    public class UpdateCompanyJobPreferencesCommandValidator : AbstractValidator<UpdateCompanyJobPreferencesCommand>
    {
        public UpdateCompanyJobPreferencesCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id is required");
        }
    }
}

