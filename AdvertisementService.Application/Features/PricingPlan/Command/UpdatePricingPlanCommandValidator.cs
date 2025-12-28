using FluentValidation;

namespace AdvertisementService.Application.Features.PricingPlan.Command
{
    public class UpdatePricingPlanCommandValidator : AbstractValidator<UpdatePricingPlanCommand>
    {
        public UpdatePricingPlanCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}

