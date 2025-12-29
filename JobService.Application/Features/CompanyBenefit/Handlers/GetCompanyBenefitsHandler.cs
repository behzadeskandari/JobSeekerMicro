using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobService.Application.Features.CompanyBenefit.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.CompanyBenefit.Handlers
{
    public class GetCompanyBenefitsHandler : IRequestHandler<GetCompanyBenefitsQuery, IEnumerable<JobService.Domain.Entities.CompanyBenefit>>
    {
        private readonly IJobUnitOfWork _repository;

        public GetCompanyBenefitsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobService.Domain.Entities.CompanyBenefit>> Handle(GetCompanyBenefitsQuery request, CancellationToken cancellationToken)
        {
            var benefits = await _repository.CompanyBenefit.GetAllAsync(cancellationToken);
            
            if (request.CompanyId.HasValue)
            {
                return benefits.Where(b => b.CompanyId == request.CompanyId.Value);
            }

            return benefits;
        }
    }
}

