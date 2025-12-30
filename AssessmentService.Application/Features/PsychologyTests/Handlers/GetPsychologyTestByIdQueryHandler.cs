using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssessmentService.Application.Features.PsychologyTests.Queries;
using AssessmentService.Application.Interfaces;
using AutoMapper;
using FluentResults;
using JobSeeker.Application.Feature.PsychologyTests.Queries;
using JobSeeker.Shared.Contracts.PsychologyTest;
using MediatR;

namespace AssessmentService.Application.Features.PsychologyTests.Handlers
{
    public class GetPsychologyTestByIdQueryHandler : IRequestHandler<GetPsychologyTestByIdQuery, Result<PsychologyTestDto>>
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPsychologyTestByIdQueryHandler(IAssessmentServiceUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PsychologyTestDto>> Handle(GetPsychologyTestByIdQuery request, CancellationToken cancellationToken)
        {
            var test = await _unitOfWork.PsychologyTestsRepository
                .GetByIdAsync(request.Id);

            if (test == null || test.IsActive != true)
                return Result.Fail("تست شخصیت پیدا نشد و یا غیر فعال است");

            var testDto = _mapper.Map<PsychologyTestDto>(test);
            return Result.Ok(testDto);
        }
    }
}
