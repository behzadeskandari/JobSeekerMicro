using FluentValidation;

namespace AdvertisementService.Application.Features.PricingCategory.Command
{
    public class CreatePricingCategoryCommandValidator : AbstractValidator<CreatePricingCategoryCommand>
    {
        public CreatePricingCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}

