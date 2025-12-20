using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobSeeker.Shared.Contracts.Candidate;
using MediatR;

namespace ProfileService.Application.Features.Candidate.Queries.GetCandidateByIdQuery
{
    public class GetCandidateByIdQuery : IRequest<Result<CandidateGetDto>>
    {
        public Guid Id { get; set; }
    }
}
