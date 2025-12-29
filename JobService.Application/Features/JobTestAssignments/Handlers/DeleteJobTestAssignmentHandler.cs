using System.Threading.Tasks;
using JobService.Application.Features.JobTestAssignments.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobTestAssignments.Handlers
{
    public class DeleteJobTestAssignmentHandler : IRequestHandler<DeleteJobTestAssignmentCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public DeleteJobTestAssignmentHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteJobTestAssignmentCommand request, CancellationToken cancellationToken)
        {
            var assignment = await _repository.JobTestAssignments.GetByIdAsync(request.Id);
            if (assignment == null)
            {
                return Result.Fail("JobTestAssignment not found");
            }

            await _repository.JobTestAssignments.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

