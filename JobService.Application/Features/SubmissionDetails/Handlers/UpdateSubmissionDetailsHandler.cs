using System;
using System.Threading.Tasks;
using JobService.Application.Features.SubmissionDetails.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.SubmissionDetails.Handlers
{
    public class UpdateSubmissionDetailsHandler : IRequestHandler<UpdateSubmissionDetailsCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public UpdateSubmissionDetailsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateSubmissionDetailsCommand request, CancellationToken cancellationToken)
        {
            var submission = await _repository.SubmissionDetails.GetByIdAsync(request.Id);
            if (submission == null)
            {
                return Result.Fail("SubmissionDetails not found");
            }

            if (request.Source != null)
                submission.Source = request.Source;
            if (request.IpAddress != null)
                submission.IpAddress = request.IpAddress;
            if (request.UserAgent != null)
                submission.UserAgent = request.UserAgent;

            submission.DateModified = DateTime.UtcNow;

            await _repository.SubmissionDetails.UpdateAsync(submission);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

