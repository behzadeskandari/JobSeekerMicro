using FluentValidation;

namespace JobService.Application.Features.CompanyBenefit.Command
{
    public class UpdateCompanyBenefitCommandValidator : AbstractValidator<UpdateCompanyBenefitCommand>
    {
        public UpdateCompanyBenefitCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id is required");
        }
    }
}

