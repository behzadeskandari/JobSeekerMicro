using AssessmentService.Application.Interfaces;
using AutoMapper;
using FluentResults;
using JobSeeker.Application.Feature.PsychologyTests.Queries;
using JobSeeker.Shared.Contracts.PsychologyTestResult;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobSeeker.Application.Feature.PsychologyTests.Handlers
{
    public class GetPsychologyTestResultsQueryHandler : IRequestHandler<GetPsychologyTestResultsQuery, Result<IEnumerable<PsychologyTestResultDto>>>
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPsychologyTestResultsQueryHandler(IAssessmentServiceUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<PsychologyTestResultDto>>> Handle(GetPsychologyTestResultsQuery request, CancellationToken cancellationToken)
        {
            var results = await _unitOfWork.PsychologyTestResultsRepository
                .GetQueryable()
                .Include(ptr => ptr.PsychologyTest)
                .Where(ptr => ptr.UserId == request.UserId && ptr.IsActive == true)
                .ToListAsync(cancellationToken);

            var resultDtos = _mapper.Map<IEnumerable<PsychologyTestResultDto>>(results);
            return Result.Ok(resultDtos);
        }
    }
}
