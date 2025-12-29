using System.Collections.Generic;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobPost.Queries
{
    public class GetJobPostsQuery : IRequest<IEnumerable<JobService.Domain.Entities.JobPost>>
    {
    }
}

