using System;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobApplication.Queries
{
    public class GetJobApplicationByIdQuery : IRequest<JobService.Domain.Entities.JobApplication?>
    {
        public int Id { get; set; }
    }
}

