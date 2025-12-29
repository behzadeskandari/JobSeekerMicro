using FluentValidation;

namespace JobService.Application.Features.OfferDetails.Command
{
    public class CreateOfferDetailsCommandValidator : AbstractValidator<CreateOfferDetailsCommand>
    {
        public CreateOfferDetailsCommandValidator()
        {
            RuleFor(x => x.ApplicationId).GreaterThan(0).WithMessage("ApplicationId must be greater than 0");
            RuleFor(x => x.OfferedById).NotEmpty().WithMessage("OfferedById is required");
            RuleFor(x => x.CompanyId).GreaterThan(0).WithMessage("CompanyId must be greater than 0");
            RuleFor(x => x.OfferDate).NotEmpty().WithMessage("OfferDate is required");
            RuleFor(x => x.Salary).GreaterThan(0).WithMessage("Salary must be greater than 0");
        }
    }
}

