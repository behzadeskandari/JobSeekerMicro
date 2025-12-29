using FluentValidation;

namespace JobService.Application.Features.InterviewDetail.Command
{
    public class UpdateInterviewDetailCommandValidator : AbstractValidator<UpdateInterviewDetailCommand>
    {
        public UpdateInterviewDetailCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id is required");
        }
    }
}

