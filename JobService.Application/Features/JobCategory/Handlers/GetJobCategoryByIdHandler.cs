using System.Threading.Tasks;
using JobService.Application.Features.JobCategory.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobCategory.Handlers
{
    public class GetJobCategoryByIdHandler : IRequestHandler<GetJobCategoryByIdQuery, JobService.Domain.Entities.JobCategory?>
    {
        private readonly IJobUnitOfWork _repository;

        public GetJobCategoryByIdHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<JobService.Domain.Entities.JobCategory?> Handle(GetJobCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.JobCategory.GetByIdAsync(request.Id);
        }
    }
}

