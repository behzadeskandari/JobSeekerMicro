using FluentValidation;

namespace JobService.Application.Features.TechnicalOptions.Command
{
    public class CreateTechnicalOptionCommandValidator : AbstractValidator<CreateTechnicalOptionCommand>
    {
        public CreateTechnicalOptionCommandValidator()
        {
            RuleFor(x => x.Label).NotEmpty().WithMessage("Label is required");
            RuleFor(x => x.Value).NotEmpty().WithMessage("Value is required");
        }
    }
}

