using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobSeeker.Shared.Contracts.Candidate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileService.Application.Features.Candidate.Queries.GetCandidateByIdQuery;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.Candidates.Handlers.GetCandidateByIdQueryHandler
{
    public class GetCandidateByIdQueryHandler : IRequestHandler<GetCandidateByIdQuery, Result<CandidateGetDto>>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;
        public GetCandidateByIdQueryHandler(IProfileServiceUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
        public async Task<Result<CandidateGetDto>> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.CandidateRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (result != null)
            {
                Result.Ok(result);
            }
            return Result.Fail("اطالاعات مورد نظر یافت نشد");
        }
    }
}
