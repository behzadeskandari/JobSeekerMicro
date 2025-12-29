using System;
using System.Threading.Tasks;
using JobService.Application.Features.Job.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.Job.Handlers
{
    public class CreateJobHandler : IRequestHandler<CreateJobCommand, Result<string>>
    {
        private readonly IJobUnitOfWork _repository;

        public CreateJobHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var company = await _repository.Company.GetByIdAsync(request.CompanyId);
            if (company == null)
            {
                return Result.Fail("Company not found");
            }

            var jobCategory = await _repository.JobCategory.GetByIdAsync(request.JobCategoryId);
            if (jobCategory == null)
            {
                return Result.Fail("JobCategory not found");
            }

            var job = new JobService.Domain.Entities.Job
            {
                Title = request.Title,
                Level = request.Level,
                CompanyId = request.CompanyId,
                IsProirity = request.IsProirity,
                JobType = request.JobType,
                JobDescription = request.JobDescription,
                JobRequirment = request.JobRequirment,
                CityId = request.CityId,
                TechnicalOptionsId = request.TechnicalOptionsId,
                JobCategoryId = request.JobCategoryId,
                Status = request.Status,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            await _repository.JobsRepository.AddAsync(job);
            await _repository.CommitAsync(cancellationToken);

            job.RaiseDomainEvent(new JobCreatedEvent(
                job.Id,
                job.Title,
                job.CompanyId,
                job.JobCategoryId,
                job.CityId,
                DateTime.UtcNow));

            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(job.Id.ToString());
        }
    }
}

