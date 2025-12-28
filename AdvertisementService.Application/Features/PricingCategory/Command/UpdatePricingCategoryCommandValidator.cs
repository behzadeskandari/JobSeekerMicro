using FluentValidation;

namespace AdvertisementService.Application.Features.PricingCategory.Command
{
    public class UpdatePricingCategoryCommandValidator : AbstractValidator<UpdatePricingCategoryCommand>
    {
        public UpdatePricingCategoryCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }
}

