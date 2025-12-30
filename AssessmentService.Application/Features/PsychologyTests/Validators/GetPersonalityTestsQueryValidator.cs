using FluentValidation;
using JobSeeker.Application.Feature.PsychologyTests.Queries;

namespace AssessmentService.Application.Features.PsychologyTests.Validators
{
    public class GetPersonalityTestsQueryValidator : AbstractValidator<GetPersonalityTestsQuery>
    {
        public GetPersonalityTestsQueryValidator()
        {
            // No validation needed
        }
    }
}
