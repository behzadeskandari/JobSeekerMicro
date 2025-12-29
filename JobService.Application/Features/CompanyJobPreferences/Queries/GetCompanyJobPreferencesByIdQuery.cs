using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.CompanyJobPreferences.Queries
{
    public class GetCompanyJobPreferencesByIdQuery : IRequest<JobService.Domain.Entities.CompanyJobPreferences?>
    {
        public int Id { get; set; }
    }
}

