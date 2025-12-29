using System.Threading.Tasks;
using JobService.Application.Features.JobPost.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobPost.Handlers
{
    public class GetJobPostByIdHandler : IRequestHandler<GetJobPostByIdQuery, JobService.Domain.Entities.JobPost?>
    {
        private readonly IJobUnitOfWork _repository;

        public GetJobPostByIdHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<JobService.Domain.Entities.JobPost?> Handle(GetJobPostByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.JobPostsRepository.GetByIdAsync(request.Id);
        }
    }
}

