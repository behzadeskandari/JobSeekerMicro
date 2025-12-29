using System.Collections.Generic;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobTestAssignments.Queries
{
    public class GetJobTestAssignmentsQuery : IRequest<IEnumerable<JobTestAssignment>>
    {
        public int? JobId { get; set; }
    }
}

