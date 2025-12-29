using System.Collections.Generic;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.SubmissionDetails.Queries
{
    public class GetSubmissionDetailsQuery : IRequest<IEnumerable<JobService.Domain.Entities.SubmissionDetails>>
    {
        public int? ApplicationId { get; set; }
    }
}

