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
    public class GetAllMBTIResultsHandler : IRequestHandler<GetAllMBTIResultsQuery, Result<List<MBTIResultDTO>>>
    {
        private readonly IAssessmentServiceUnitOfWork _unitOfWork;

        public GetAllMBTIResultsHandler(IAssessmentServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<MBTIResultDTO>>> Handle(GetAllMBTIResultsQuery request, CancellationToken cancellationToken)
        {
            // Fetch all MBTI results from the repository
            var results = await _unitOfWork.MBTIResultsRepository.GetAllAsyncMBTI();

            // Map the results to DTOs
            var dtos = results.Select(r => new MBTIResultDTO
            {
                Id = r.Id,
                Name = r.Name,
                Type = r.Type,
                Description = r.Description,
                Result = r.Result
            }).ToList();

            // Return the DTOs wrapped in a FluentResults object
            return Result.Ok(dtos);
        }
    }
}
