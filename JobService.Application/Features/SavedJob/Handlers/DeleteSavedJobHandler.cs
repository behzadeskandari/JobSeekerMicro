using System;
using System.Threading.Tasks;
using JobService.Application.Features.SavedJob.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.SavedJob.Handlers
{
    public class DeleteSavedJobHandler : IRequestHandler<DeleteSavedJobCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public DeleteSavedJobHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteSavedJobCommand request, CancellationToken cancellationToken)
        {
            var savedJob = await _repository.SavedJob.GetByIdAsync(request.Id);
            if (savedJob == null)
            {
                return Result.Fail("SavedJob not found");
            }

            savedJob.RaiseDomainEvent(new SavedJobRemovedEvent(
                savedJob.Id,
                savedJob.UserId,
                savedJob.JobPostId,
                DateTime.UtcNow));

            await _repository.SavedJob.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

