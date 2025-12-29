using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssessmentService.Application.Features.MBTIQuestions.Command;
using AssessmentService.Application.Interfaces;
using AssessmentService.Domain.Entities;
using FluentResults;
using JobSeeker.Shared.Contracts.MbtiTest;
using JobSeeker.Shared.Kernel.Abstractions;
using MediatR;

namespace AssessmentService.Application.Features.MBTIQuestions.Handlers
{
    public class CreateMBTIQuestionHandler : IRequestHandler<CreateMBTIQuestionCommand, Result<MBTIQuestionDTO>>
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;

        public CreateMBTIQuestionHandler(IAssessmentServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MBTIQuestionDTO>> Handle(CreateMBTIQuestionCommand request, CancellationToken cancellationToken)
        {
            var entity = new AssessmentService.Domain.Entities.MBTIQuestions
            {
                QuestionText = request.MBTIQuestion.QuestionText,
                Category = request.MBTIQuestion.Category,
                IsActive = request.MBTIQuestion.IsActive
            };

            await _unitOfWork.MBTIQuestionsRepository.AddAsyncMBTI(entity);
            await _unitOfWork.CommitAsync(cancellationToken);

            request.MBTIQuestion.Id = entity.Id;
            return Result.Ok(request.MBTIQuestion);
        }
    }
}
