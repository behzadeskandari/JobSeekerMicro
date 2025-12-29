using FluentValidation;

namespace JobService.Application.Features.JobTestAssignments.Command
{
    public class UpdateJobTestAssignmentCommandValidator : AbstractValidator<UpdateJobTestAssignmentCommand>
    {
        public UpdateJobTestAssignmentCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id is required");
        }
    }
}

