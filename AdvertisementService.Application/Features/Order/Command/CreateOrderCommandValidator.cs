using FluentValidation;

namespace AdvertisementService.Application.Features.Order.Command
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.PricingPlanId).GreaterThan(0).WithMessage("PricingPlanId must be greater than 0");
            RuleFor(x => x.TotalAmount).GreaterThan(0).WithMessage("TotalAmount must be greater than 0");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");
        }
    }
}

