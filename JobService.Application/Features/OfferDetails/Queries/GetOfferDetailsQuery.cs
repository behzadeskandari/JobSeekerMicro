using System.Collections.Generic;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.OfferDetails.Queries
{
    public class GetOfferDetailsQuery : IRequest<IEnumerable<JobService.Domain.Entities.OfferDetails>>
    {
        public int? ApplicationId { get; set; }
        public int? CompanyId { get; set; }
    }
}

