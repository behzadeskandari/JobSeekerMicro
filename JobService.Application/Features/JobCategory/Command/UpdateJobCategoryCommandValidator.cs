using FluentValidation;

namespace JobService.Application.Features.JobCategory.Command
{
    public class UpdateJobCategoryCommandValidator : AbstractValidator<UpdateJobCategoryCommand>
    {
        public UpdateJobCategoryCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id is required");
        }
    }
}

