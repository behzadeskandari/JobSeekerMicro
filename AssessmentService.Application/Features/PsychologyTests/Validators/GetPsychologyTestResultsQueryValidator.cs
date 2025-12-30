using FluentValidation;
using JobSeeker.Application.Feature.PsychologyTests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentService.Application.Features.PsychologyTests.Validators
{
    public class GetPsychologyTestResultsQueryValidator : AbstractValidator<GetPsychologyTestResultsQuery>
    {
        public GetPsychologyTestResultsQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
        }
    }
}
