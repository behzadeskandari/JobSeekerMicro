using System.Collections.Generic;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.CompanyJobPreferences.Queries
{
    public class GetCompanyJobPreferencesQuery : IRequest<IEnumerable<JobService.Domain.Entities.CompanyJobPreferences>>
    {
        public int? JobPostId { get; set; }
    }
}

