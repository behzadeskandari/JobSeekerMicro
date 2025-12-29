using System.Threading.Tasks;
using JobService.Application.Features.Job.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.Job.Handlers
{
    public class GetJobByIdHandler : IRequestHandler<GetJobByIdQuery, JobService.Domain.Entities.Job?>
    {
        private readonly IJobUnitOfWork _repository;

        public GetJobByIdHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<JobService.Domain.Entities.Job?> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.JobsRepository.GetByIdAsync(request.Id);
        }
    }
}

