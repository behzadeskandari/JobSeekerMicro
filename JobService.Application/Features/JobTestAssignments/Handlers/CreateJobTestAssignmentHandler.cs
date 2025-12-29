using System;
using System.Threading.Tasks;
using JobService.Application.Features.JobTestAssignments.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobTestAssignments.Handlers
{
    public class CreateJobTestAssignmentHandler : IRequestHandler<CreateJobTestAssignmentCommand, Result<int>>
    {
        private readonly IJobUnitOfWork _repository;

        public CreateJobTestAssignmentHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CreateJobTestAssignmentCommand request, CancellationToken cancellationToken)
        {
            if (request.JobId.HasValue)
            {
                var job = await _repository.JobsRepository.GetByIdAsync(request.JobId.Value);
                if (job == null)
                {
                    return Result.Fail("Job not found");
                }
            }

            var assignment = new JobTestAssignment
            {
                JobId = request.JobId,
                PsychologyTestId = request.PsychologyTestId,
                PersonalityTestId = request.PersonalityTestId,
                IsRequired = request.IsRequired,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            assignment.RaiseDomainEvent(new JobTestAssignmentCreatedEvent(
                assignment.Id,
                assignment.JobId,
                assignment.IsRequired,
                DateTime.UtcNow));

            await _repository.JobTestAssignments.AddAsync(assignment);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(assignment.Id);
        }
    }
}

