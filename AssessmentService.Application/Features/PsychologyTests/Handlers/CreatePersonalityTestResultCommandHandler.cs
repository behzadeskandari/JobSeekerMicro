using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssessmentService.Application.Features.PsychologyTests.Command;
using AssessmentService.Application.Interfaces;
using AutoMapper;
using FluentResults;
using JobSeeker.Shared.Contracts.PersonalityTestResult;
using MediatR;

namespace AssessmentService.Application.Features.PsychologyTests.Handlers
{
    public class CreatePersonalityTestResultCommandHandler : IRequestHandler<CreatePersonalityTestResultCommand, Result<PersonalityTestResultDto>>
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePersonalityTestResultCommandHandler(IAssessmentServiceUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PersonalityTestResultDto>> Handle(CreatePersonalityTestResultCommand request, CancellationToken cancellationToken)
        {
            var test = await _unitOfWork.PersonalityTraitsRepository.GetByIdAsync(request.PersonalityTestId);
            if (test == null || test.IsActive != true)
                return Result.Fail("تست شخصیت شناسی پیدا نشد");

            var user = await _unitOfWork.PersonalityTraitsRepository.GetByIdAsync(request.UserId);
            if (user == null)
                return Result.Fail("کاربر پیدا نشد");

            var result = new AssessmentService.Domain.Entities.PersonalityTestResults
            {
                //PersonalityTestId = request.PersonalityTestId,
                UserId = request.UserId,
                //ResultData = request.ResultData,
                //DateTaken = DateTime.Now,
                IsActive = true
            };
            //:TODO
            //await _unitOfWork.WriteRepository<PersonalityTestResult>().AddAsync(result);
            //await _unitOfWork.CompleteAsync();

            var resultDto = _mapper.Map<PersonalityTestResultDto>(result);
            return Result.Ok(resultDto);
        }
    }
}
