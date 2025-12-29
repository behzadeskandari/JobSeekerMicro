using System.Collections.Generic;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.Company.Queries
{
    public class GetCompaniesQuery : IRequest<IEnumerable<JobService.Domain.Entities.Company>>
    {
    }
}

