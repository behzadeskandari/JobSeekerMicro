using FluentValidation;

namespace JobService.Application.Features.RejectionDetails.Command
{
    public class CreateRejectionDetailsCommandValidator : AbstractValidator<CreateRejectionDetailsCommand>
    {
        public CreateRejectionDetailsCommandValidator()
        {
            RuleFor(x => x.ApplicationId).GreaterThan(0).WithMessage("ApplicationId must be greater than 0");
            RuleFor(x => x.RejectedById).NotEmpty().WithMessage("RejectedById is required");
            RuleFor(x => x.RejectionDate).NotEmpty().WithMessage("RejectionDate is required");
        }
    }
}

