using System;
using System.Threading.Tasks;
using JobService.Application.Features.CompanyJobPreferences.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.CompanyJobPreferences.Handlers
{
    public class CreateCompanyJobPreferencesHandler : IRequestHandler<CreateCompanyJobPreferencesCommand, Result<int>>
    {
        private readonly IJobUnitOfWork _repository;

        public CreateCompanyJobPreferencesHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CreateCompanyJobPreferencesCommand request, CancellationToken cancellationToken)
        {
            var jobPost = await _repository.JobPostsRepository.GetByIdAsync(request.JobPostId);
            if (jobPost == null)
            {
                return Result.Fail("JobPost not found");
            }

            var preference = new JobService.Domain.Entities.CompanyJobPreferences
            {
                JobPostId = request.JobPostId,
                PreferredSkills = request.PreferredSkills,
                PreferredEducationLevel = request.PreferredEducationLevel,
                PreferredExperienceLevel = request.PreferredExperienceLevel,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            await _repository.CompanyJobPreferences.AddAsync(preference);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(preference.Id);
        }
    }
}

