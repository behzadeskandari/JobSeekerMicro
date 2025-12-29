using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobService.Application.Features.JobTestAssignments.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.JobTestAssignments.Handlers
{
    public class GetJobTestAssignmentsHandler : IRequestHandler<GetJobTestAssignmentsQuery, IEnumerable<JobTestAssignment>>
    {
        private readonly IJobUnitOfWork _repository;

        public GetJobTestAssignmentsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobTestAssignment>> Handle(GetJobTestAssignmentsQuery request, CancellationToken cancellationToken)
        {
            var assignments = await _repository.JobTestAssignments.GetAllAsync(cancellationToken);

            if (request.JobId.HasValue)
            {
                return assignments.Where(a => a.JobId == request.JobId.Value);
            }

            return assignments;
        }
    }
}

