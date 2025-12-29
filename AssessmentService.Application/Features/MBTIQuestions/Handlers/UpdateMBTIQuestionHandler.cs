using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssessmentService.Application.Features.MBTIQuestions.Command;
using AssessmentService.Application.Interfaces;
using FluentResults;
using JobSeeker.Shared.Contracts.MbtiTest;
using JobSeeker.Shared.Kernel.Abstractions;
using MediatR;

namespace AssessmentService.Application.Features.MBTIQuestions.Handlers
{
    public class UpdateMBTIQuestionHandler : IRequestHandler<UpdateMBTIQuestionCommand, Result<MBTIQuestionDTO>>
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;

        public UpdateMBTIQuestionHandler(IAssessmentServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MBTIQuestionDTO>> Handle(UpdateMBTIQuestionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.MBTIQuestionsRepository.GetByIdAsyncMBTI(request.Id);
            if (entity == null)
                Result.Fail("سوال MBTI پیدا نشد");

            entity.QuestionText = request.MBTIQuestion.QuestionText;
            entity.Category = request.MBTIQuestion.Category;
            entity.IsActive = request.MBTIQuestion.IsActive;

            _unitOfWork.MBTIQuestionsRepository.UpdateMBTI(entity);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok(request.MBTIQuestion);
        }
    }
}
