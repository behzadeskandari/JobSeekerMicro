using System.Threading.Tasks;
using JobService.Application.Features.JobTestAssignments.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobTestAssignments.Handlers
{
    public class GetJobTestAssignmentByIdHandler : IRequestHandler<GetJobTestAssignmentByIdQuery, JobTestAssignment?>
    {
        private readonly IJobUnitOfWork _repository;

        public GetJobTestAssignmentByIdHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<JobTestAssignment?> Handle(GetJobTestAssignmentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.JobTestAssignments.GetByIdAsync(request.Id);
        }
    }
}

