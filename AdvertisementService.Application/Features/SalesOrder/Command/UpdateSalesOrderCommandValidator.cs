using FluentValidation;

namespace AdvertisementService.Application.Features.SalesOrder.Command
{
    public class UpdateSalesOrderCommandValidator : AbstractValidator<UpdateSalesOrderCommand>
    {
        public UpdateSalesOrderCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}

