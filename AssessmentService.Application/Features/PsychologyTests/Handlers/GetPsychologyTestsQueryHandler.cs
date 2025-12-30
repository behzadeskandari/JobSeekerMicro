using AssessmentService.Application.Interfaces;
using AutoMapper;
using FluentResults;
using JobSeeker.Application.Feature.PsychologyTests.Queries;
using JobSeeker.Shared.Contracts.PsychologyTest;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobSeeker.Application.Feature.PsychologyTests.Handlers
{
    public class GetPsychologyTestsQueryHandler : IRequestHandler<GetPsychologyTestsQuery, Result<IEnumerable<PsychologyTestDto>>>
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPsychologyTestsQueryHandler(IAssessmentServiceUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<PsychologyTestDto>>> Handle(GetPsychologyTestsQuery request, CancellationToken cancellationToken)
        {
            var tests = await _unitOfWork.PsychologyTestsRepository
                .GetQueryable()
                .Where(pt => pt.IsActive == true)
                .ToListAsync(cancellationToken);

            var testDtos = _mapper.Map<IEnumerable<PsychologyTestDto>>(tests);
            return Result.Ok(testDtos);
        }
    }
}
