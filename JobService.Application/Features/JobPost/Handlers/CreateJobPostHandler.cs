using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobService.Application.Features.JobPost.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobSeeker.Shared.Contracts.IntegrationEvents;
using JobSeeker.Shared.EventBusRabbitMQ;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobPost.Handlers
{
    public class CreateJobPostHandler : IRequestHandler<CreateJobPostCommand, Result<string>>
    {
        private readonly IJobUnitOfWork _repository;
        private readonly IEventBus _eventBus;

        public CreateJobPostHandler(IJobUnitOfWork repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task<Result<string>> Handle(CreateJobPostCommand request, CancellationToken cancellationToken)
        {
            var company = await _repository.Company.GetByIdAsync(request.CompanyId);
            if (company == null)
            {
                return Result.Fail("Company not found");
            }

            var city = await _repository.City.GetByIdAsync(request.CityId);
            if (city == null)
            {
                return Result.Fail("City not found");
            }

            var jobPost = new JobService.Domain.Entities.JobPost
            {
                Title = request.Title,
                Description = request.Description,
                Requirements = request.Requirements,
                BenefitId = request.BenefitId,
                Location = request.Location,
                Salary = request.Salary,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = request.ExpiresAt ?? DateTime.UtcNow.AddDays(60),
                IsActive = false,
                JobId = request.JobId,
                Source = request.Source,
                ExternalJobBoardId = request.ExternalJobBoardId,
                SyncStatus = request.SyncStatus,
                LastSyncDate = request.LastSyncDate,
                CompanyJobPreferenceIds = request.CompanyJobPreferenceIds,
                SkillIds = request.SkillIds,
                MinimumExperience = request.MinimumExperience,
                MinimumEducationLevelId = request.MinimumEducationLevelId,
                MinimumEducationLevelDegree = request.MinimumEducationLevelDegree,
                MinimumEducationLevelInstitution = request.MinimumEducationLevelInstitution,
                MinimumEducationLevelField = request.MinimumEducationLevelField,
                MinimumEducationLevelDescription = request.MinimumEducationLevelDescription,
                CityId = request.CityId,
                JobCategoryId = request.JobCategoryId,
                ProvinceId = request.ProvinceId,
                CompanyId = request.CompanyId,
                DateCreated = DateTime.UtcNow,
            };

            await _repository.JobPostsRepository.AddAsync(jobPost);
            await _repository.CommitAsync(cancellationToken);

            // Publish integration event for job post creation
            var jobPostPublishedEvent = new JobPostPublishedIntegrationEvent();

            jobPostPublishedEvent.JobPostId = int.Parse(jobPost.Id.ToString());
            jobPostPublishedEvent.CompanyId = request.CompanyId;
            jobPostPublishedEvent.Title = request.Title;
            jobPostPublishedEvent.JobCategoryId = request.JobCategoryId.Value;
            jobPostPublishedEvent.ProvinceId = request.ProvinceId;
            jobPostPublishedEvent.CityId = request.CityId;
            jobPostPublishedEvent.UserId = request.UserId;
            jobPostPublishedEvent.PublishedAt = DateTime.UtcNow;

            await _eventBus.PublishAsync(jobPostPublishedEvent);

            return Result.Ok(jobPost.Id.ToString());
        }
    }
}

