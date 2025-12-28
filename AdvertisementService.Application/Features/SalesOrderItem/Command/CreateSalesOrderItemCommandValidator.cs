using FluentValidation;

namespace AdvertisementService.Application.Features.SalesOrderItem.Command
{
    public class CreateSalesOrderItemCommandValidator : AbstractValidator<CreateSalesOrderItemCommand>
    {
        public CreateSalesOrderItemCommandValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("ProductId must be greater than 0");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
}

