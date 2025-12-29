using System.Collections.Generic;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.CompanyBenefit.Queries
{
    public class GetCompanyBenefitsQuery : IRequest<IEnumerable<JobService.Domain.Entities.CompanyBenefit>>
    {
        public int? CompanyId { get; set; }
    }
}

