using System;
using System.Threading.Tasks;
using JobService.Application.Features.SubmissionDetails.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.SubmissionDetails.Handlers
{
    public class CreateSubmissionDetailsHandler : IRequestHandler<CreateSubmissionDetailsCommand, Result<int>>
    {
        private readonly IJobUnitOfWork _repository;

        public CreateSubmissionDetailsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CreateSubmissionDetailsCommand request, CancellationToken cancellationToken)
        {
            var application = await _repository.JobApplication.GetByIdAsync(request.ApplicationId);
            if (application == null)
            {
                return Result.Fail("JobApplication not found");
            }

            var submission = new JobService.Domain.Entities.SubmissionDetails
            {
                //Id = Guid.NewGuid(),
                ApplicationId = request.ApplicationId,
                Source = request.Source,
                IpAddress = request.IpAddress,
                UserAgent = request.UserAgent,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            submission.RaiseDomainEvent(new SubmissionDetailsCreatedEvent(
                submission.Id,
                submission.ApplicationId,
                submission.Source,
                DateTime.UtcNow));

            await _repository.SubmissionDetails.AddAsync(submission);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(submission.Id);
        }
    }
}

