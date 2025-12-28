using FluentValidation;

namespace AdvertisementService.Application.Features.Customer.Command
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
        }
    }
}

