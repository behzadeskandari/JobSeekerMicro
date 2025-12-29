using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobCategory.Queries
{
    public class GetJobCategoryByIdQuery : IRequest<JobService.Domain.Entities.JobCategory?>
    {
        public int Id { get; set; }
    }
}

