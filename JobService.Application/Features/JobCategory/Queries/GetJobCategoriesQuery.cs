using System.Collections.Generic;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobCategory.Queries
{
    public class GetJobCategoriesQuery : IRequest<IEnumerable<JobService.Domain.Entities.JobCategory>>
    {
    }
}

