using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobService.Application.Features.CompanyJobPreferences.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.CompanyJobPreferences.Handlers
{
    public class GetCompanyJobPreferencesHandler : IRequestHandler<GetCompanyJobPreferencesQuery, IEnumerable<JobService.Domain.Entities.CompanyJobPreferences>>
    {
        private readonly IJobUnitOfWork _repository;

        public GetCompanyJobPreferencesHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobService.Domain.Entities.CompanyJobPreferences>> Handle(GetCompanyJobPreferencesQuery request, CancellationToken cancellationToken)
        {
            var preferences = await _repository.CompanyJobPreferences.GetAllAsync(cancellationToken);

            if (request.JobPostId.HasValue)
            {
                return preferences.Where(p => p.JobPostId == request.JobPostId.Value);
            }

            return preferences;
        }
    }
}

