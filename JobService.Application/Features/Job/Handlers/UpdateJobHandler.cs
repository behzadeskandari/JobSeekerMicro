using System;
using System.Threading.Tasks;
using JobService.Application.Features.Job.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.Job.Handlers
{
    public class UpdateJobHandler : IRequestHandler<UpdateJobCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public UpdateJobHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _repository.JobsRepository.GetByIdAsync(request.Id);
            if (job == null)
            {
                return Result.Fail("Job not found");
            }

            if (!string.IsNullOrEmpty(request.Title))
                job.Title = request.Title;
            if (request.Level.HasValue)
                job.Level = request.Level.Value;
            if (request.CompanyId.HasValue)
                job.CompanyId = request.CompanyId.Value;
            if (request.IsProirity.HasValue)
                job.IsProirity = request.IsProirity.Value;
            if (request.JobType.HasValue)
                job.JobType = request.JobType.Value;
            if (request.JobDescription != null)
                job.JobDescription = request.JobDescription;
            if (request.JobRequirment != null)
                job.JobRequirment = request.JobRequirment;
            if (request.CityId.HasValue)
                job.CityId = request.CityId;
            if (request.TechnicalOptionsId.HasValue)
                job.TechnicalOptionsId = request.TechnicalOptionsId;
            if (request.JobCategoryId.HasValue)
                job.JobCategoryId = request.JobCategoryId.Value;
            if (request.Status.HasValue)
                job.Status = request.Status.Value;

            job.DateModified = DateTime.UtcNow;

            job.RaiseDomainEvent(new JobUpdatedEvent(
                job.Id,
                job.Title,
                job.CompanyId,
                DateTime.UtcNow));

            await _repository.JobsRepository.UpdateAsync(job);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

