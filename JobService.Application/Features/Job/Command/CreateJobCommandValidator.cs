using FluentValidation;

namespace JobService.Application.Features.Job.Command
{
    public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
    {
        public CreateJobCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.CompanyId).GreaterThan(0).WithMessage("CompanyId must be greater than 0");
            RuleFor(x => x.JobCategoryId).GreaterThan(0).WithMessage("JobCategoryId must be greater than 0");
        }
    }
}

