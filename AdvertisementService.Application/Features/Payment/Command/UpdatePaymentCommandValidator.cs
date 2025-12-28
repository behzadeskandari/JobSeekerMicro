using FluentValidation;

namespace AdvertisementService.Application.Features.Payment.Command
{
    public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
    {
        public UpdatePaymentCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}

