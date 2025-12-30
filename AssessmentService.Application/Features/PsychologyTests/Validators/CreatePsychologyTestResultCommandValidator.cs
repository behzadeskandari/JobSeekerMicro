using AssessmentService.Application.Features.PsychologyTests.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentService.Application.Features.PsychologyTests.Validators
{
    public class CreatePsychologyTestResultCommandValidator : AbstractValidator<CreatePsychologyTestResultCommand>
    {
        public CreatePsychologyTestResultCommandValidator()
        {
            RuleFor(x => x.PsychologyTestId).GreaterThan(0).WithMessage("Psychology test ID is required");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required");
            RuleFor(x => x.ResultData).NotEmpty().WithMessage("Result data is required");
            RuleFor(x => x.TotalScore).GreaterThanOrEqualTo(0).WithMessage("Total score cannot be negative");
        }
    }
}
