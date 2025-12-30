using AssessmentService.Application.Interfaces;
using AutoMapper;
using FluentResults;
using JobSeeker.Application.Feature.PsychologyTests.Queries;
using JobSeeker.Shared.Contracts.PersonalityTest;
using MediatR;

namespace AssessmentService.Application.Features.PsychologyTests.Handlers
{
    public class GetPersonalityTestByIdQueryHandler : IRequestHandler<GetPersonalityTestByIdQuery, Result<PersonalityTestDto>>
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPersonalityTestByIdQueryHandler(IAssessmentServiceUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PersonalityTestDto>> Handle(GetPersonalityTestByIdQuery request, CancellationToken cancellationToken)
        {
            var test = await _unitOfWork.PersonalityTraitsRepository
                .GetByIdAsync(request.Id);

            if (test == null || test.IsActive != true)
                return Result.Fail("تست شخصیت پیدا نشد و یا غیر فعال است");

            var testDto = _mapper.Map<PersonalityTestDto>(test);
            return Result.Ok(testDto);
        }
    }
}
