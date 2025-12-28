using FluentValidation;

namespace AdvertisementService.Application.Features.PricingFeature.Command
{
    public class UpdatePricingFeatureCommandValidator : AbstractValidator<UpdatePricingFeatureCommand>
    {
        public UpdatePricingFeatureCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}

