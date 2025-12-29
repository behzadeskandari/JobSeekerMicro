using FluentValidation;

namespace JobService.Application.Features.CompanyFollow.Command
{
    public class CreateCompanyFollowCommandValidator : AbstractValidator<CreateCompanyFollowCommand>
    {
        public CreateCompanyFollowCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
            RuleFor(x => x.CompanyId).GreaterThan(0).WithMessage("CompanyId must be greater than 0");
        }
    }
}

