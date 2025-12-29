using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.InterviewDetail.Queries
{
    public class GetInterviewDetailByIdQuery : IRequest<JobService.Domain.Entities.InterviewDetail?>
    {
        public int Id { get; set; }
    }
}

