using FluentValidation;

namespace JobService.Application.Features.RejectionDetails.Command
{
    public class UpdateRejectionDetailsCommandValidator : AbstractValidator<UpdateRejectionDetailsCommand>
    {
        public UpdateRejectionDetailsCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}

