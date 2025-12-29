using FluentValidation;

namespace JobService.Application.Features.CompanyFollow.Command
{
    public class UpdateCompanyFollowCommandValidator : AbstractValidator<UpdateCompanyFollowCommand>
    {
        public UpdateCompanyFollowCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id is required");
        }
    }
}

