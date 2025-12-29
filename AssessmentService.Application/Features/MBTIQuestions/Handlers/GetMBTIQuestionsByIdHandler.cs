using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssessmentService.Application.Features.MBTIQuestions.Queries;
using AssessmentService.Application.Interfaces;
using FluentResults;
using JobSeeker.Shared.Contracts.MbtiTest;
using JobSeeker.Shared.Kernel.Abstractions;
using MediatR;

namespace AssessmentService.Application.Features.MBTIQuestions.Handlers
{
    public class GetMBTIQuestionsByIdHandler : IRequestHandler<GetMBTIQuestionsByIdQuery, Result<MBTIQuestionDTO>>
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;
        public GetMBTIQuestionsByIdHandler(IAssessmentServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MBTIQuestionDTO>> Handle(GetMBTIQuestionsByIdQuery request, CancellationToken cancellationToken)
        {
            // Fetch the MBTIQuestion by ID from the repository
            var entity = await _unitOfWork.MBTIQuestionsRepository.GetByIdAsyncMBTI(request.Id);
            if (entity == null)
                Result.Fail("سوال MBTI پیدا نشد");

            // Map the entity to a DTO
            var dto = new MBTIQuestionDTO
            {
                Id = entity.Id,
                QuestionText = entity.QuestionText,
                Category = entity.Category,
                IsActive = entity.IsActive
            };

            // Return the DTO wrapped in a FluentResults object
            return Result.Ok(dto);
        }
    }
}
