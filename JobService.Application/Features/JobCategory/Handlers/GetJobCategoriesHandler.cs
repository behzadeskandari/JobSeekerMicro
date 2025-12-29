using System.Collections.Generic;
using System.Threading.Tasks;
using JobService.Application.Features.JobCategory.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobCategory.Handlers
{
    public class GetJobCategoriesHandler : IRequestHandler<GetJobCategoriesQuery, IEnumerable<JobService.Domain.Entities.JobCategory>>
    {
        private readonly IJobUnitOfWork _repository;

        public GetJobCategoriesHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobService.Domain.Entities.JobCategory>> Handle(GetJobCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.JobCategory.GetAllAsync(cancellationToken);
        }
    }
}

