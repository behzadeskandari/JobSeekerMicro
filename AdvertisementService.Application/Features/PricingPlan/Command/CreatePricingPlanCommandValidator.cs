using FluentValidation;

namespace AdvertisementService.Application.Features.PricingPlan.Command
{
    public class CreatePricingPlanCommandValidator : AbstractValidator<CreatePricingPlanCommand>
    {
        public CreatePricingPlanCommandValidator()
        {
            RuleFor(x => x.PricingCategoryId).GreaterThan(0).WithMessage("PricingCategoryId must be greater than 0");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0");
        }
    }
}

