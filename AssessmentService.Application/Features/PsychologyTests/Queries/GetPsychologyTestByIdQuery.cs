using FluentResults;
using JobSeeker.Shared.Contracts.PsychologyTest;

namespace AssessmentService.Application.Features.PsychologyTests.Queries
{
    public class GetPsychologyTestByIdQuery : MediatR.IRequest<Result<PsychologyTestDto>>
    {
        public int Id { get; set; }
    }
}
