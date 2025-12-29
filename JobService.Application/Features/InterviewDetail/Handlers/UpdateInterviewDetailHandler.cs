using System;
using System.Threading.Tasks;
using JobService.Application.Features.InterviewDetail.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.InterviewDetail.Handlers
{
    public class UpdateInterviewDetailHandler : IRequestHandler<UpdateInterviewDetailCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public UpdateInterviewDetailHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateInterviewDetailCommand request, CancellationToken cancellationToken)
        {
            var interview = await _repository.InterviewDetail.GetByIdAsync(request.Id);
            if (interview == null)
            {
                return Result.Fail("InterviewDetail not found");
            }

            if (request.Date.HasValue)
                interview.Date = request.Date.Value;
            if (!string.IsNullOrEmpty(request.Type))
                interview.Type = request.Type;
            if (request.Notes != null)
                interview.Notes = request.Notes;
            if (request.Outcome != null)
                interview.Outcome = request.Outcome;

            interview.DateModified = DateTime.UtcNow;

            interview.RaiseDomainEvent(new InterviewDetailUpdatedEvent(
                interview.Id,
                interview.ApplicationId,
                interview.Outcome,
                DateTime.UtcNow));

            await _repository.InterviewDetail.UpdateAsync(interview);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

