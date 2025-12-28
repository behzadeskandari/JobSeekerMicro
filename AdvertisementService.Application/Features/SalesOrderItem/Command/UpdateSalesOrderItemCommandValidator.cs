using FluentValidation;

namespace AdvertisementService.Application.Features.SalesOrderItem.Command
{
    public class UpdateSalesOrderItemCommandValidator : AbstractValidator<UpdateSalesOrderItemCommand>
    {
        public UpdateSalesOrderItemCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}

