using System.Threading.Tasks;
using JobService.Application.Features.SavedJob.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.SavedJob.Handlers
{
    public class GetSavedJobByIdHandler : IRequestHandler<GetSavedJobByIdQuery, JobService.Domain.Entities.SavedJob?>
    {
        private readonly IJobUnitOfWork _repository;

        public GetSavedJobByIdHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<JobService.Domain.Entities.SavedJob?> Handle(GetSavedJobByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.SavedJob.GetByIdAsync(request.Id);
        }
    }
}

