using FluentValidation;

namespace JobService.Application.Features.CompanyBenefit.Command
{
    public class CreateCompanyBenefitCommandValidator : AbstractValidator<CreateCompanyBenefitCommand>
    {
        public CreateCompanyBenefitCommandValidator()
        {
            RuleFor(x => x.CompanyId).GreaterThan(0).WithMessage("CompanyId must be greater than 0");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}

