using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobService.Application.Features.JobPost.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobPost.Handlers
{
    public class CreateJobPostHandler : IRequestHandler<CreateJobPostCommand, Result<string>>
    {
        private readonly IJobUnitOfWork _repository;

        public CreateJobPostHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
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

            return Result.Ok(jobPost.Id.ToString());
        }
    }
}

