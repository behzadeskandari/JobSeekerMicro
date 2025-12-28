using FluentValidation;

namespace AdvertisementService.Application.Features.Product.Command
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(64).WithMessage("Name is required and must be max 64 characters");
            RuleFor(x => x.Description).NotEmpty().MaximumLength(128).WithMessage("Description is required and must be max 128 characters");
            RuleFor(x => x.Sku).NotEmpty().WithMessage("Sku is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(x => x.Cost).GreaterThan(0).WithMessage("Cost must be greater than 0");
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("CategoryId must be greater than 0");
        }
    }
}

