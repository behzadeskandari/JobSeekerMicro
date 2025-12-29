using System;
using System.Threading.Tasks;
using JobService.Application.Features.JobRequest.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobRequest.Handlers
{
    public class UpdateJobRequestHandler : IRequestHandler<UpdateJobRequestCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public UpdateJobRequestHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateJobRequestCommand request, CancellationToken cancellationToken)
        {
            var jobRequest = await _repository.JobRequestsRepository.GetByIdAsync(request.Id);
            if (jobRequest == null)
            {
                return Result.Fail("JobRequest not found");
            }

            if (!string.IsNullOrEmpty(request.CoverLetter))
                jobRequest.CoverLetter = request.CoverLetter;
            if (!string.IsNullOrEmpty(request.ResumeUrl))
                jobRequest.ResumeUrl = request.ResumeUrl;
            if (request.Status.HasValue)
                jobRequest.Status = request.Status.Value;

            jobRequest.DateModified = DateTime.UtcNow;

            await _repository.JobRequestsRepository.UpdateAsync(jobRequest);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

