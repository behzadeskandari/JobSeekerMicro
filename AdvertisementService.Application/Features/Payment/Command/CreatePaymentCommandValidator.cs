using FluentValidation;

namespace AdvertisementService.Application.Features.Payment.Command
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(x => x.AdvertisementId).GreaterThan(0).WithMessage("AdvertisementId must be greater than 0");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0");
            RuleFor(x => x.TransactionId).NotEmpty().WithMessage("TransactionId is required");
        }
    }
}

