using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobService.Application.Features.SavedJob.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.SavedJob.Handlers
{
    public class GetSavedJobsHandler : IRequestHandler<GetSavedJobsQuery, IEnumerable<JobService.Domain.Entities.SavedJob>>
    {
        private readonly IJobUnitOfWork _repository;

        public GetSavedJobsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobService.Domain.Entities.SavedJob>> Handle(GetSavedJobsQuery request, CancellationToken cancellationToken)
        {
            var savedJobs = await _repository.SavedJob.GetSavedJobAsync(request,cancellationToken);

            

            return savedJobs;
        }
    }
}

