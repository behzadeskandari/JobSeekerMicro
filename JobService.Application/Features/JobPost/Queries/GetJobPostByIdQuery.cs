using System;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobPost.Queries
{
    public class GetJobPostByIdQuery : IRequest<JobService.Domain.Entities.JobPost?>
    {
        public int Id { get; set; }
    }
}

