using AssessmentService.Domain.Entities;
using FluentResults;
using JobSeeker.Shared.Contracts.PsychologyTestResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentService.Application.Features.PsychologyTests.Command
{
    public class CreatePsychologyTestResultCommand : MediatR.IRequest<Result<PsychologyTestResultDto>>
    {
        public int PsychologyTestId { get; set; }
        public string UserId { get; set; }
        public decimal TotalScore { get; set; }
        public PsychologyTestInterpretation Interpretation { get; set; }
        public string ResultData { get; set; }
    }
}
