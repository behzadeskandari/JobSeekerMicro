using System.Collections.Generic;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.CompanyFollow.Queries
{
    public class GetCompanyFollowsQuery : IRequest<IEnumerable<JobService.Domain.Entities.CompanyFollow>>
    {
        public string? UserId { get; set; }
        public int? CompanyId { get; set; }
    }
}

