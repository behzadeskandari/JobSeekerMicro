using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobRequest.Queries
{
    public class GetJobRequestByIdQuery : IRequest<JobService.Domain.Entities.JobRequest?>
    {
        public int Id { get; set; }
    }
}

