using System;
using System.Threading.Tasks;
using JobService.Application.Features.SavedJob.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.SavedJob.Handlers
{
    public class CreateSavedJobHandler : IRequestHandler<CreateSavedJobCommand, Result<int>>
    {
        private readonly IJobUnitOfWork _repository;

        public CreateSavedJobHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CreateSavedJobCommand request, CancellationToken cancellationToken)
        {
            var jobPost = await _repository.JobPostsRepository.GetByIdAsync(request.JobPostId);
            if (jobPost == null)
            {
                return Result.Fail("JobPost not found");
            }

            var savedJob = new JobService.Domain.Entities.SavedJob
            {
                //Id = Guid.NewGuid(),
                JobPostId = request.JobPostId,
                UserId = request.UserId,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            savedJob.RaiseDomainEvent(new SavedJobAddedEvent(
                savedJob.Id,
                savedJob.UserId,
                savedJob.JobPostId,
                DateTime.UtcNow));

            await _repository.SavedJob.AddAsync(savedJob);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(savedJob.Id);
        }
    }
}

