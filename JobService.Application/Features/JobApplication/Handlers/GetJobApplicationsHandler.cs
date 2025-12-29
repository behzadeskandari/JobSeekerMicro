using System.Collections.Generic;
using System.Threading.Tasks;
using JobService.Application.Features.JobApplication.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobApplication.Handlers
{
    public class GetJobApplicationsHandler : IRequestHandler<GetJobApplicationsQuery, IEnumerable<JobService.Domain.Entities.JobApplication>>
    {
        private readonly IJobUnitOfWork _repository;

        public GetJobApplicationsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobService.Domain.Entities.JobApplication>> Handle(GetJobApplicationsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.JobApplication.GetAllAsync(cancellationToken);
        }
    }
}

