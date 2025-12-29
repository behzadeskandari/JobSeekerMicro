using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobService.Application.Features.JobRequest.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobRequest.Handlers
{
    public class GetJobRequestsHandler : IRequestHandler<GetJobRequestsQuery, IEnumerable<JobService.Domain.Entities.JobRequest>>
    {
        private readonly IJobUnitOfWork _repository;

        public GetJobRequestsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobService.Domain.Entities.JobRequest>> Handle(GetJobRequestsQuery request, CancellationToken cancellationToken)
        {
            var jobRequests = await _repository.JobRequestsRepository.GetJobRequestByUserId(request,cancellationToken);
            return jobRequests;
        }
    }
}

