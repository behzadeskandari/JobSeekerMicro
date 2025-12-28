using FluentValidation;

namespace AdvertisementService.Application.Features.Feature.Command
{
    public class CreateFeatureCommandValidator : AbstractValidator<CreateFeatureCommand>
    {
        public CreateFeatureCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.IconName).NotEmpty().WithMessage("IconName is required");
            RuleFor(x => x.Language).NotEmpty().WithMessage("Language is required");
        }
    }
}

