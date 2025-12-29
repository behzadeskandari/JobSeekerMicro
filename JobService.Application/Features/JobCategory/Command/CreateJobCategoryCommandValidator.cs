using FluentValidation;

namespace JobService.Application.Features.JobCategory.Command
{
    public class CreateJobCategoryCommandValidator : AbstractValidator<CreateJobCategoryCommand>
    {
        public CreateJobCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}

