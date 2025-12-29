using FluentValidation;

namespace JobService.Application.Features.SavedJob.Command
{
    public class CreateSavedJobCommandValidator : AbstractValidator<CreateSavedJobCommand>
    {
        public CreateSavedJobCommandValidator()
        {
            RuleFor(x => x.JobPostId).GreaterThan(0).WithMessage("JobPostId must be greater than 0");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
        }
    }
}

