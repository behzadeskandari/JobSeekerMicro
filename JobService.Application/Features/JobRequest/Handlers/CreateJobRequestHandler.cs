using System;
using System.Threading.Tasks;
using JobService.Application.Features.JobRequest.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobRequest.Handlers
{
    public class CreateJobRequestHandler : IRequestHandler<CreateJobRequestCommand, Result<int>>
    {
        private readonly IJobUnitOfWork _repository;

        public CreateJobRequestHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CreateJobRequestCommand request, CancellationToken cancellationToken)
        {
            var jobPost = await _repository.JobPostsRepository.GetByIdAsync(request.JobPostId);
            if (jobPost == null)
            {
                return Result.Fail("JobPost not found");
            }

            var jobRequest = new JobService.Domain.Entities.JobRequest
            {
                UserId = request.UserId,
                JobPostId = request.JobPostId,
                CoverLetter = request.CoverLetter,
                ResumeUrl = request.ResumeUrl,
                Status = request.Status,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            jobRequest.RaiseDomainEvent(new JobRequestSubmittedEvent(
                jobRequest.Id,
                jobRequest.UserId,
                jobRequest.JobPostId,
                DateTime.UtcNow));

            await _repository.JobRequestsRepository.AddAsync(jobRequest);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(jobRequest.Id);
        }
    }
}

