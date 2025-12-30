using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssessmentService.Application.Interfaces;
using AutoMapper;
using FluentResults;
using JobSeeker.Application.Feature.PsychologyTests.Queries;
using JobSeeker.Shared.Contracts.PsychologyTestResult;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AssessmentService.Application.Features.PsychologyTests.Handlers
{
    public class GetPsychologyTestResultByIdQueryHandler : IRequestHandler<GetPsychologyTestResultByIdQuery, Result<PsychologyTestResultDto>>
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPsychologyTestResultByIdQueryHandler(IAssessmentServiceUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PsychologyTestResultDto>> Handle(GetPsychologyTestResultByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.PsychologyTestResultsRepository.GetQueryable()
                .Include(ptr => ptr.PsychologyTest)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (result == null || result.IsActive != true)
                return Result.Fail("تست شخصیت پیدا نشد و یا غیر فعال است");

            if (result.UserId != request.UserId)
                return Result.Fail("دسترسی غیرمجاز به نتایج آزمایش");

            var resultDto = _mapper.Map<PsychologyTestResultDto>(result);
            return Result.Ok(resultDto);
        }
    }
}
