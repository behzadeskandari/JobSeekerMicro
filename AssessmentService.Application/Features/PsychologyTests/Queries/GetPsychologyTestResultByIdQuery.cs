using FluentResults;
using JobSeeker.Shared.Contracts.PsychologyTestResult;

namespace JobSeeker.Application.Feature.PsychologyTests.Queries
{
    public class GetPsychologyTestResultByIdQuery : MediatR.IRequest<Result<PsychologyTestResultDto>>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
    }
}
