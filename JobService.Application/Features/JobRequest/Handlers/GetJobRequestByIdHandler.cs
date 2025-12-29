using System.Threading.Tasks;
using JobService.Application.Features.JobRequest.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobRequest.Handlers
{
    public class GetJobRequestByIdHandler : IRequestHandler<GetJobRequestByIdQuery, JobService.Domain.Entities.JobRequest?>
    {
        private readonly IJobUnitOfWork _repository;

        public GetJobRequestByIdHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<JobService.Domain.Entities.JobRequest?> Handle(GetJobRequestByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.JobRequestsRepository.GetByIdAsync(request.Id);
        }
    }
}

