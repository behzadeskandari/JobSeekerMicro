using FluentResults;
using JobSeeker.Shared.Contracts.PsychologyTest;

namespace JobSeeker.Application.Feature.PsychologyTests.Queries
{
    public class GetPsychologyTestsQuery : MediatR.IRequest<Result<IEnumerable<PsychologyTestDto>>>
    {
    }
}
