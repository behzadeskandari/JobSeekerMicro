using FluentValidation;

namespace JobService.Application.Features.TechnicalOptions.Command
{
    public class UpdateTechnicalOptionCommandValidator : AbstractValidator<UpdateTechnicalOptionCommand>
    {
        public UpdateTechnicalOptionCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id is required");
        }
    }
}

