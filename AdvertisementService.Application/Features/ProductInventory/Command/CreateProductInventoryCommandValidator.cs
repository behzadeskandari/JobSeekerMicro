using FluentValidation;

namespace AdvertisementService.Application.Features.ProductInventory.Command
{
    public class CreateProductInventoryCommandValidator : AbstractValidator<CreateProductInventoryCommand>
    {
        public CreateProductInventoryCommandValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("ProductId must be greater than 0");
            RuleFor(x => x.QuantityOnHand).GreaterThanOrEqualTo(0).WithMessage("QuantityOnHand must be greater than or equal to 0");
            RuleFor(x => x.IdealQuantity).GreaterThanOrEqualTo(0).WithMessage("IdealQuantity must be greater than or equal to 0");
        }
    }
}

