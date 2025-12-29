using System.Threading.Tasks;
using JobService.Application.Features.CompanyBenefit.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.CompanyBenefit.Handlers
{
    public class GetCompanyBenefitByIdHandler : IRequestHandler<GetCompanyBenefitByIdQuery,JobService.Domain.Entities.CompanyBenefit?>
    {
        private readonly IJobUnitOfWork _repository;

        public GetCompanyBenefitByIdHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<JobService.Domain.Entities.CompanyBenefit?> Handle(GetCompanyBenefitByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.CompanyBenefit.GetByIdAsync(request.Id);
        }
    }
}

