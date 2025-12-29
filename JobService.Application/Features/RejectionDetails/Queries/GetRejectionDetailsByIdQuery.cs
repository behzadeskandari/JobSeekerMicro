using System;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.RejectionDetails.Queries
{
    public class GetRejectionDetailsByIdQuery : IRequest<JobService.Domain.Entities.RejectionDetails?>
    {
        public Guid Id { get; set; }
    }
}

