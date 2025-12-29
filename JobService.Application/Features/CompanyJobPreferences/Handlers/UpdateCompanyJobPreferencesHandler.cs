using System;
using System.Threading.Tasks;
using JobService.Application.Features.CompanyJobPreferences.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.CompanyJobPreferences.Handlers
{
    public class UpdateCompanyJobPreferencesHandler : IRequestHandler<UpdateCompanyJobPreferencesCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public UpdateCompanyJobPreferencesHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateCompanyJobPreferencesCommand request, CancellationToken cancellationToken)
        {
            var preference = await _repository.CompanyJobPreferences.GetByIdAsync(request.Id);
            if (preference == null)
            {
                return Result.Fail("CompanyJobPreferences not found");
            }

            if (request.PreferredSkills != null)
                preference.PreferredSkills = request.PreferredSkills;
            if (request.PreferredEducationLevel != null)
                preference.PreferredEducationLevel = request.PreferredEducationLevel;
            if (request.PreferredExperienceLevel != null)
                preference.PreferredExperienceLevel = request.PreferredExperienceLevel;

            preference.DateModified = DateTime.UtcNow;

            preference.RaiseDomainEvent(new CompanyJobPreferencesUpdatedEvent(
                preference.Id,
                preference.JobPostId,
                DateTime.UtcNow));

            await _repository.CompanyJobPreferences.UpdateAsync(preference);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

