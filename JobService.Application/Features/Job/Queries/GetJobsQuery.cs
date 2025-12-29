using System.Collections.Generic;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.Job.Queries
{
    public class GetJobsQuery : IRequest<IEnumerable<JobService.Domain.Entities.Job>>
    {
    }
}

