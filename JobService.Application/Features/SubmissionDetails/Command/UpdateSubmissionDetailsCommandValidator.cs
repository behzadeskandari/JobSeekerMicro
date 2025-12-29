using FluentValidation;

namespace JobService.Application.Features.SubmissionDetails.Command
{
    public class UpdateSubmissionDetailsCommandValidator : AbstractValidator<UpdateSubmissionDetailsCommand>
    {
        public UpdateSubmissionDetailsCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}

