using System;
using System.Threading.Tasks;
using JobService.Application.Features.JobApplication.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobApplication.Handlers
{
    public class UpdateJobApplicationHandler : IRequestHandler<UpdateJobApplicationCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public UpdateJobApplicationHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateJobApplicationCommand request, CancellationToken cancellationToken)
        {
            var jobApplication = await _repository.JobApplication.GetByIdAsync(request.Id);
            if (jobApplication == null)
            {
                return Result.Fail("JobApplication not found");
            }

            if (!string.IsNullOrEmpty(request.Status))
                jobApplication.Status = request.Status;
            if (request.CoverLetter != null)
                jobApplication.CoverLetter = request.CoverLetter;
            if (request.Notes != null)
                jobApplication.Notes = request.Notes;

            jobApplication.DateModified = DateTime.UtcNow;

            jobApplication.RaiseDomainEvent(new JobApplicationUpdatedEvent(
                jobApplication.Id,
                jobApplication.Status,
                jobApplication.DateModified ?? DateTime.UtcNow));

            await _repository.JobApplication.UpdateAsync(jobApplication);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

