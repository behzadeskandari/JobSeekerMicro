using FluentValidation;

namespace JobService.Application.Features.OfferDetails.Command
{
    public class UpdateOfferDetailsCommandValidator : AbstractValidator<UpdateOfferDetailsCommand>
    {
        public UpdateOfferDetailsCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id is required");
        }
    }
}

