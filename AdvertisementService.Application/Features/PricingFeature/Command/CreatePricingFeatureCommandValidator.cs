using FluentValidation;

namespace AdvertisementService.Application.Features.PricingFeature.Command
{
    public class CreatePricingFeatureCommandValidator : AbstractValidator<CreatePricingFeatureCommand>
    {
        public CreatePricingFeatureCommandValidator()
        {
            RuleFor(x => x.PricingPlanId).GreaterThan(0).WithMessage("PricingPlanId must be greater than 0");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.IconName).NotEmpty().WithMessage("IconName is required");
        }
    }
}

