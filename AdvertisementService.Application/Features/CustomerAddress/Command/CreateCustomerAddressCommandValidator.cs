using FluentValidation;

namespace AdvertisementService.Application.Features.CustomerAddress.Command
{
    public class CreateCustomerAddressCommandValidator : AbstractValidator<CreateCustomerAddressCommand>
    {
        public CreateCustomerAddressCommandValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage("CustomerId must be greater than 0");
            RuleFor(x => x.AddressLine1).NotEmpty().WithMessage("AddressLine1 is required");
        }
    }
}

