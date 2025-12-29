using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssessmentService.Application.Features.MBTIResults.Queries;
using AssessmentService.Application.Interfaces;
using FluentResults;
using JobSeeker.Shared.Contracts.MbtiTest;
using JobSeeker.Shared.Kernel.Abstractions;
using MediatR;

namespace AssessmentService.Application.Features.MBTIResults.Handlers
{
    public class GetMBTIResultHandler : IRequestHandler<GetMBTIResultQuery, Result<MBTIResultDTO>>
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;

        public GetMBTIResultHandler(IAssessmentServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<MBTIResultDTO>> Handle(GetMBTIResultQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.MBTIResultsRepository.GetByIdAsyncMBTI(request.Id);
            if (entity == null)
                Result.Fail("نتیجه MBTI یافت نشد");

            var dto = new MBTIResultDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Type = entity.Type,
                Description = entity.Description,
                Result = entity.Result
            };

            return Result.Ok(dto);
        }
    }
}
