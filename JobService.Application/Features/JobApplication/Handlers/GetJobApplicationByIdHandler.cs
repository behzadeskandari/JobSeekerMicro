using System.Threading.Tasks;
using JobService.Application.Features.JobApplication.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobApplication.Handlers
{
    public class GetJobApplicationByIdHandler : IRequestHandler<GetJobApplicationByIdQuery, JobService.Domain.Entities.JobApplication?>
    {
        private readonly IJobUnitOfWork _repository;

        public GetJobApplicationByIdHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<JobService.Domain.Entities.JobApplication?> Handle(GetJobApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.JobApplication.GetByIdAsync(request.Id);
        }
    }
}

