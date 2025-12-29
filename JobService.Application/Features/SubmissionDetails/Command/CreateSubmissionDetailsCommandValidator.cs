using FluentValidation;

namespace JobService.Application.Features.SubmissionDetails.Command
{
    public class CreateSubmissionDetailsCommandValidator : AbstractValidator<CreateSubmissionDetailsCommand>
    {
        public CreateSubmissionDetailsCommandValidator()
        {
            RuleFor(x => x.ApplicationId).GreaterThan(0).WithMessage("ApplicationId must be greater than 0");
        }
    }
}

