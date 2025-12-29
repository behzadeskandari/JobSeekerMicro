using FluentValidation;

namespace JobService.Application.Features.JobRequest.Command
{
    public class CreateJobRequestCommandValidator : AbstractValidator<CreateJobRequestCommand>
    {
        public CreateJobRequestCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(x => x.JobPostId).GreaterThan(0).WithMessage("JobPostId must be greater than 0");
            RuleFor(x => x.CoverLetter).NotEmpty().WithMessage("CoverLetter is required");
            RuleFor(x => x.ResumeUrl).NotEmpty().WithMessage("ResumeUrl is required");
        }
    }
}

