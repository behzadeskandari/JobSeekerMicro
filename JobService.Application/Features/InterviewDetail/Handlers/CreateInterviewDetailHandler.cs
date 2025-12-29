using System;
using System.Threading.Tasks;
using JobService.Application.Features.InterviewDetail.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.InterviewDetail.Handlers
{
    public class CreateInterviewDetailHandler : IRequestHandler<CreateInterviewDetailCommand, Result<int>>
    {
        private readonly IJobUnitOfWork _repository;

        public CreateInterviewDetailHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CreateInterviewDetailCommand request, CancellationToken cancellationToken)
        {
            var application = await _repository.JobApplication.GetByIdAsync(request.ApplicationId);
            if (application == null)
            {
                return Result.Fail("JobApplication not found");
            }

            var interview = new JobService.Domain.Entities.InterviewDetail
            {
                ApplicationId = request.ApplicationId,
                InterviewerId = request.InterviewerId,
                Date = request.Date,
                Type = request.Type,
                Notes = request.Notes,
                Outcome = request.Outcome,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            interview.RaiseDomainEvent(new InterviewDetailCreatedEvent(
                interview.Id,
                interview.ApplicationId,
                interview.InterviewerId,
                interview.Date,
                interview.Type,
                DateTime.UtcNow));

            await _repository.InterviewDetail.AddAsync(interview);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(interview.Id);
        }
    }
}

