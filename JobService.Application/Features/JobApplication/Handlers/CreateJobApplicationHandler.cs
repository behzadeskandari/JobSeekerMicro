using System;
using System.Threading.Tasks;
using JobService.Application.Features.JobApplication.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobApplication.Handlers
{
    public class CreateJobApplicationHandler : IRequestHandler<CreateJobApplicationCommand, Result<string>>
    {
        private readonly IJobUnitOfWork _repository;

        public CreateJobApplicationHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CreateJobApplicationCommand request, CancellationToken cancellationToken)
        {
            var job = await _repository.JobsRepository.GetByIdAsync(request.JobId);
            if (job == null)
            {
                return Result.Fail("Job not found");
            }

            var jobPost = await _repository.JobPostsRepository.GetByIdAsync(request.JobPostId);
            if (jobPost == null)
            {
                return Result.Fail("JobPost not found");
            }

            var jobApplication = new JobService.Domain.Entities.JobApplication
            {
                JobId = request.JobId,
                JobPostId = request.JobPostId,
                UserId = request.UserId,
                ApplicationDate = DateTime.UtcNow,
                ResumeFileName = request.ResumeFileName,
                ResumeFileUrl = request.ResumeFileUrl,
                CoverLetter = request.CoverLetter,
                Status = request.Status,
                Notes = request.Notes,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            await _repository.JobApplication.AddAsync(jobApplication);
            await _repository.CommitAsync(cancellationToken);

            jobApplication.RaiseDomainEvent(new JobApplicationSubmittedEvent(
                jobApplication.Id,
                jobApplication.JobPostId,
                jobApplication.UserId,
                jobApplication.ResumeFileUrl,
                jobApplication.ApplicationDate));

            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(jobApplication.Id.ToString());
        }
    }
}

