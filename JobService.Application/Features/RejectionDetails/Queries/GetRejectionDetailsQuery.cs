using System.Collections.Generic;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.RejectionDetails.Queries
{
    public class GetRejectionDetailsQuery : IRequest<IEnumerable<JobService.Domain.Entities.RejectionDetails>>
    {
        public int? ApplicationId { get; set; }
    }
}

