using FluentValidation;

namespace JobService.Application.Features.Province.Command
{
    public class CreateProvinceCommandValidator : AbstractValidator<CreateProvinceCommand>
    {
        public CreateProvinceCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
            RuleFor(x => x.Label).NotEmpty().WithMessage("Label is required");
            RuleFor(x => x.Value).NotEmpty().WithMessage("Value is required");
        }
    }
}

