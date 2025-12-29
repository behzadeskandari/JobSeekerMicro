using System.Threading.Tasks;
using JobService.Application.Features.CompanyJobPreferences.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.CompanyJobPreferences.Handlers
{
    public class GetCompanyJobPreferencesByIdHandler : IRequestHandler<GetCompanyJobPreferencesByIdQuery, JobService.Domain.Entities.CompanyJobPreferences?>
    {
        private readonly IJobUnitOfWork _repository;

        public GetCompanyJobPreferencesByIdHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<JobService.Domain.Entities.CompanyJobPreferences?> Handle(GetCompanyJobPreferencesByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.CompanyJobPreferences.GetByIdAsync(request.Id);
        }
    }
}

