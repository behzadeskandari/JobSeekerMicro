using FluentValidation;

namespace AdvertisementService.Application.Features.CustomerAddress.Command
{
    public class UpdateCustomerAddressCommandValidator : AbstractValidator<UpdateCustomerAddressCommand>
    {
        public UpdateCustomerAddressCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}

