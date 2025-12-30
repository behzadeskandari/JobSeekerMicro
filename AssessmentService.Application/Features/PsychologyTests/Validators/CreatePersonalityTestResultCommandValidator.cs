using AssessmentService.Application.Features.PsychologyTests.Command;
using FluentValidation;

namespace AssessmentService.Application.Features.PsychologyTests.Validators
{
    public class CreatePersonalityTestResultCommandValidator : AbstractValidator<CreatePersonalityTestResultCommand>
    {
        public CreatePersonalityTestResultCommandValidator()
        {
            RuleFor(x => x.PersonalityTestId).GreaterThan(0).WithMessage("Personality test ID is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
            RuleFor(x => x.ResultData).NotEmpty().WithMessage("Result data is required");
        }
    }
}
