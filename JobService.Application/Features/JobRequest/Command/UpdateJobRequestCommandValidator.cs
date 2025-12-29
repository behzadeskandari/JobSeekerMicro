using FluentValidation;

namespace JobService.Application.Features.JobRequest.Command
{
    public class UpdateJobRequestCommandValidator : AbstractValidator<UpdateJobRequestCommand>
    {
        public UpdateJobRequestCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id is required");
        }
    }
}

