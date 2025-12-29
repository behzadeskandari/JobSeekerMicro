using FluentValidation;

namespace JobService.Application.Features.Province.Command
{
    public class UpdateProvinceCommandValidator : AbstractValidator<UpdateProvinceCommand>
    {
        public UpdateProvinceCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id is required");
        }
    }
}

