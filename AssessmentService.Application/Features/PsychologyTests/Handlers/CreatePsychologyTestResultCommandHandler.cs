using AssessmentService.Application.Features.PsychologyTests.Command;
using AssessmentService.Application.Interfaces;
using AssessmentService.Domain.Entities;
using AutoMapper;
using FluentResults;
using JobSeeker.Shared.Contracts.PsychologyTestResult;
using MediatR;

namespace AssessmentService.Application.Features.PsychologyTests.Handlers
{
    public class CreatePsychologyTestResultCommandHandler : IRequestHandler<CreatePsychologyTestResultCommand, Result<PsychologyTestResultDto>>
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePsychologyTestResultCommandHandler(IAssessmentServiceUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PsychologyTestResultDto>> Handle(CreatePsychologyTestResultCommand request, CancellationToken cancellationToken)
        {
            var test = await _unitOfWork.PsychologyTestsRepository
                .GetByIdAsync(request.PsychologyTestId);
            if (test == null || test.IsActive != true)
                return Result.Fail("تست روانشناسی پیدا نشد");

            var user = await _unitOfWork.PsychologyTestsRepository.GetByIdAsync(request.UserId);
            if (user == null)
                return Result.Fail("کاربر پیدا نشد");

            var result = new PsychologyTestResult
            {
                PsychologyTestId = request.PsychologyTestId,
                UserId = request.UserId,
                TotalScore = request.TotalScore,
                ResultData = request.ResultData,
                SubmissionDate = DateTime.Now,
                DateTaken = DateTime.Now,
                IsActive = true
            };
            result.Interpretation.Add(request.Interpretation);

            await _unitOfWork.PsychologyTestResultsRepository.AddAsync(result);
            await _unitOfWork.CommitAsync(cancellationToken);

            var resultDto = _mapper.Map<PsychologyTestResultDto>(result);
            return Result.Ok(resultDto);
        }
    }

}
