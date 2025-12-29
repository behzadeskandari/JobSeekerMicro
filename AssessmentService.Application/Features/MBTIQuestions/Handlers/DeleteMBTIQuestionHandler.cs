using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssessmentService.Application.Features.MBTIQuestions.Command;
using AssessmentService.Application.Interfaces;
using FluentResults;
using JobSeeker.Shared.Kernel.Abstractions;
using MediatR;

namespace AssessmentService.Application.Features.MBTIQuestions.Handlers
{
    public class DeleteMBTIQuestionHandler : IRequestHandler<DeleteMBTIQuestionCommand, Result>
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;

        public DeleteMBTIQuestionHandler(IAssessmentServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteMBTIQuestionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.MBTIQuestionsRepository.GetByIdAsyncMBTI(request.Id);
            if (entity == null)
                Result.Fail("سوال MBTI پیدا نشد");

            _unitOfWork.MBTIQuestionsRepository.DeleteMBTI(entity);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
