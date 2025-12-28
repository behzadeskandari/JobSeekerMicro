using FluentValidation;

namespace AdvertisementService.Application.Features.Feature.Command
{
    public class UpdateFeatureCommandValidator : AbstractValidator<UpdateFeatureCommand>
    {
        public UpdateFeatureCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}

