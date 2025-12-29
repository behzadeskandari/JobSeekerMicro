using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssessmentService.Application.Features.MBTIResults.Command;
using AssessmentService.Application.Interfaces;
using FluentResults;
using JobSeeker.Shared.Contracts.MbtiTest;
using JobSeeker.Shared.Kernel.Abstractions;
using MediatR;

namespace AssessmentService.Application.Features.MBTIResults.Handlers
{
    public class UpdateMBTIResultHandler : IRequestHandler<UpdateMBTIResultCommand, Result<MBTIResultDTO>>
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;

        public UpdateMBTIResultHandler(IAssessmentServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MBTIResultDTO>> Handle(UpdateMBTIResultCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.MBTIResultsRepository.GetByIdAsyncMBTI(request.Id);
            if (entity == null)
                Result.Fail("نتیجه MBTI یافت نشد");

            entity.Name = request.MBTIResult.Name;
            entity.Type = request.MBTIResult.Type;
            entity.Description = request.MBTIResult.Description;
            entity.Result = request.MBTIResult.Result;

            _unitOfWork.MBTIResultsRepository.UpdateMBTI(entity);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok(request.MBTIResult);
        }
    }
}
