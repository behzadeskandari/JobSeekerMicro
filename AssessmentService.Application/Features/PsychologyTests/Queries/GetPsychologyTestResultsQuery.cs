using FluentResults;
using JobSeeker.Shared.Contracts.PsychologyTestResult;

namespace JobSeeker.Application.Feature.PsychologyTests.Queries
{
    public class GetPsychologyTestResultsQuery : MediatR.IRequest<Result<IEnumerable<PsychologyTestResultDto>>>
    {
        public string UserId { get; set; }
    }
}
