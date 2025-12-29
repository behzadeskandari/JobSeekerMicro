using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobTestAssignments.Queries
{
    public class GetJobTestAssignmentByIdQuery : IRequest<JobTestAssignment?>
    {
        public int Id { get; set; }
    }
}

