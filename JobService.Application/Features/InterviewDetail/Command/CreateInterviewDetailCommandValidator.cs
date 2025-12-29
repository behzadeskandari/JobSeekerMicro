using FluentValidation;

namespace JobService.Application.Features.InterviewDetail.Command
{
    public class CreateInterviewDetailCommandValidator : AbstractValidator<CreateInterviewDetailCommand>
    {
        public CreateInterviewDetailCommandValidator()
        {
            RuleFor(x => x.ApplicationId).GreaterThan(0).WithMessage("ApplicationId must be greater than 0");
            RuleFor(x => x.InterviewerId).GreaterThan(0).WithMessage("InterviewerId must be greater than 0");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Date is required");
            RuleFor(x => x.Type).NotEmpty().WithMessage("Type is required");
        }
    }
}

