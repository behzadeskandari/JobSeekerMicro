using System.Collections.Generic;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobApplication.Queries
{
    public class GetJobApplicationsQuery : IRequest<IEnumerable<JobService.Domain.Entities.JobApplication>>
    {
    }
}

