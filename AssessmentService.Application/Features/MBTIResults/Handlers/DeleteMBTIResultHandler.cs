using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssessmentService.Application.Features.MBTIResults.Command;
using AssessmentService.Application.Interfaces;
using FluentResults;
using JobSeeker.Shared.Kernel.Abstractions;
using MediatR;

namespace AssessmentService.Application.Features.MBTIResults.Handlers
{
    public class DeleteMBTIResultHandler : IRequestHandler<DeleteMBTIResultCommand, Result>
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;

        public DeleteMBTIResultHandler(IAssessmentServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteMBTIResultCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.MBTIResultsRepository.GetByIdAsyncMBTI(request.Id);
            if (entity == null)
                Result.Fail("نتیجه MBTI یافت نشد");

            _unitOfWork.MBTIResultsRepository.DeleteMBTI(entity);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
