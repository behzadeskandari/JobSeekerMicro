using System.Collections.Generic;
using System.Threading.Tasks;
using JobService.Application.Features.JobPost.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobPost.Handlers
{
    public class GetJobPostsHandler : IRequestHandler<GetJobPostsQuery, IEnumerable<JobService.Domain.Entities.JobPost>>
    {
        private readonly IJobUnitOfWork _repository;

        public GetJobPostsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobService.Domain.Entities.JobPost>> Handle(GetJobPostsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.JobPostsRepository.GetAllAsync(cancellationToken);
        }
    }
}

