using AssessmentService.Application.Features.PsychologyTests.Queries;
using FluentValidation;
using JobSeeker.Application.Feature.PsychologyTests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentService.Application.Features.PsychologyTests.Validators
{
    public class GetPsychologyTestByIdQueryValidator : AbstractValidator<GetPsychologyTestByIdQuery>
    {
        public GetPsychologyTestByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Psychology test ID is required");
        }
    }
}
