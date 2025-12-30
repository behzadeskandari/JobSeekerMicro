using FluentValidation;
using JobSeeker.Application.Feature.PsychologyTests.Queries;

namespace AssessmentService.Application.Features.PsychologyTests.Validators
{
    public class GetPersonalityTestByIdQueryValidator : AbstractValidator<GetPersonalityTestByIdQuery>
    {
        public GetPersonalityTestByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Personality test ID is required");
        }
    }
}
