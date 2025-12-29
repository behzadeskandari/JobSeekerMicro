using FluentValidation;

namespace JobService.Application.Features.JobTestAssignments.Command
{
    public class CreateJobTestAssignmentCommandValidator : AbstractValidator<CreateJobTestAssignmentCommand>
    {
        public CreateJobTestAssignmentCommandValidator()
        {
            // IsRequired is required (bool, so always has a value)
        }
    }
}

