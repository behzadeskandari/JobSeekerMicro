using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssessmentService.Application.Features.MBTIResults.Command;
using AssessmentService.Application.Interfaces;
using AssessmentService.Domain.Entities;
using FluentResults;
using JobSeeker.Shared.Contracts.MbtiTest;
using JobSeeker.Shared.Kernel.Abstractions;
using MediatR;

namespace AssessmentService.Application.Features.MBTIResults.Handlers
{
    public class CreateMBTIResultHandler : IRequestHandler<CreateMBTIResultCommand, Result<MBTIResultDTO>>
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;

        public CreateMBTIResultHandler(IAssessmentServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MBTIResultDTO>> Handle(CreateMBTIResultCommand request, CancellationToken cancellationToken)
        {
            var entity = new MBTIResult
            {
                Name = request.MBTIResult.Name,
                Type = request.MBTIResult.Type,
                Description = request.MBTIResult.Description,
                Result = request.MBTIResult.Result
            };

            await _unitOfWork.MBTIResultsRepository.AddAsyncMBTI(entity);
            await _unitOfWork.CommitAsync(cancellationToken);

            request.MBTIResult.Id = entity.Id;
            return Result.Ok(request.MBTIResult);
        }
    }
}
