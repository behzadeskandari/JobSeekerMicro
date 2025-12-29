using FluentValidation;

namespace JobService.Application.Features.JobPost.Command
{
    public class UpdateJobPostCommandValidator : AbstractValidator<UpdateJobPostCommand>
    {
        public UpdateJobPostCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}

