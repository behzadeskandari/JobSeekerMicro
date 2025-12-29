using System;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.Job.Queries
{
    public class GetJobByIdQuery : IRequest<JobService.Domain.Entities.Job?>
    {
        public int Id { get; set; }
    }
}

