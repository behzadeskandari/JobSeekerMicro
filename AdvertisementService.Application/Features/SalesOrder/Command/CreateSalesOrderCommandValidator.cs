using FluentValidation;

namespace AdvertisementService.Application.Features.SalesOrder.Command
{
    public class CreateSalesOrderCommandValidator : AbstractValidator<CreateSalesOrderCommand>
    {
        public CreateSalesOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage("CustomerId must be greater than 0");
        }
    }
}

