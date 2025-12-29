using System;
using System.Threading.Tasks;
using JobService.Application.Features.JobTestAssignments.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobTestAssignments.Handlers
{
    public class UpdateJobTestAssignmentHandler : IRequestHandler<UpdateJobTestAssignmentCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public UpdateJobTestAssignmentHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateJobTestAssignmentCommand request, CancellationToken cancellationToken)
        {
            var assignment = await _repository.JobTestAssignments.GetByIdAsync(request.Id);
            if (assignment == null)
            {
                return Result.Fail("JobTestAssignment not found");
            }

            if (request.JobId.HasValue)
                assignment.JobId = request.JobId;
            if (request.PsychologyTestId.HasValue)
                assignment.PsychologyTestId = request.PsychologyTestId;
            if (request.PersonalityTestId.HasValue)
                assignment.PersonalityTestId = request.PersonalityTestId;
            if (request.IsRequired.HasValue)
                assignment.IsRequired = request.IsRequired.Value;

            assignment.DateModified = DateTime.UtcNow;

            await _repository.JobTestAssignments.UpdateAsync(assignment);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

