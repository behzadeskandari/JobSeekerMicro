using FluentValidation;

namespace JobService.Application.Features.JobApplication.Command
{
    public class CreateJobApplicationCommandValidator : AbstractValidator<CreateJobApplicationCommand>
    {
        public CreateJobApplicationCommandValidator()
        {
            RuleFor(x => x.JobId).GreaterThan(0).WithMessage("JobId must be greater than 0");
            RuleFor(x => x.JobPostId).GreaterThan(0).WithMessage("JobPostId must be greater than 0");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(x => x.ResumeFileName).NotEmpty().WithMessage("ResumeFileName is required");
            RuleFor(x => x.ResumeFileUrl).NotEmpty().WithMessage("ResumeFileUrl is required");
        }
    }
}

