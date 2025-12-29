using System.Collections.Generic;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.Province.Queries
{
    public class GetProvincesQuery : IRequest<IEnumerable<JobService.Domain.Entities.Province>>
    {
    }
}

