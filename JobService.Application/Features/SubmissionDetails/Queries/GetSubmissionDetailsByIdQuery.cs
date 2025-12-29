using System;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.SubmissionDetails.Queries
{
    public class GetSubmissionDetailsByIdQuery : IRequest<JobService.Domain.Entities.SubmissionDetails?>
    {
        public Guid Id { get; set; }
    }
}

