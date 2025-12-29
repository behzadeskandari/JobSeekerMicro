using FluentValidation;

namespace JobService.Application.Features.JobPost.Command
{
    public class CreateJobPostCommandValidator : AbstractValidator<CreateJobPostCommand>
    {
        public CreateJobPostCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Requirements).NotEmpty().WithMessage("Requirements is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(x => x.CityId).GreaterThan(0).WithMessage("CityId must be greater than 0");
            RuleFor(x => x.CompanyId).GreaterThan(0).WithMessage("CompanyId must be greater than 0");
        }
    }
}

