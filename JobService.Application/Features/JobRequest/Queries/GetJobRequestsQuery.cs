using System.Collections.Generic;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobRequest.Queries
{
    public class GetJobRequestsQuery : IRequest<IEnumerable<JobService.Domain.Entities.JobRequest>>
    {
        public string? UserId { get; set; }
        public int? JobPostId { get; set; }
    }
}

