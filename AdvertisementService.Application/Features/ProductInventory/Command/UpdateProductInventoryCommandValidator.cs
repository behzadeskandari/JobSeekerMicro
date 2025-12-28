using FluentValidation;

namespace AdvertisementService.Application.Features.ProductInventory.Command
{
    public class UpdateProductInventoryCommandValidator : AbstractValidator<UpdateProductInventoryCommand>
    {
        public UpdateProductInventoryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}

