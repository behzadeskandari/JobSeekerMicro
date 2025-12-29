using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.CompanyBenefit.Queries
{
    public class GetCompanyBenefitByIdQuery : IRequest<JobService.Domain.Entities.CompanyBenefit?>
    {
        public int Id { get; set; }
    }
}

