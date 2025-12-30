using FluentValidation;
using JobSeeker.Application.Feature.PsychologyTests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentService.Application.Features.PsychologyTests.Validators
{
    public class GetPsychologyTestsQueryValidator : AbstractValidator<GetPsychologyTestsQuery>
    {
        public GetPsychologyTestsQueryValidator()
        {
            // No validation needed
        }
    }
}
