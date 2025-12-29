using System.Collections.Generic;
using System.Threading.Tasks;
using JobService.Application.Features.Job.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.Job.Handlers
{
    public class GetJobsHandler : IRequestHandler<GetJobsQuery, IEnumerable<JobService.Domain.Entities.Job>>
    {
        private readonly IJobUnitOfWork _repository;

        public GetJobsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobService.Domain.Entities.Job>> Handle(GetJobsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.JobsRepository.GetAllAsync(cancellationToken);
        }
    }
}

