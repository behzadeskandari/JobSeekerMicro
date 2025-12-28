using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.PricingPlan.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.PricingPlan.Handlers
{
    public class GetPricingPlansHandler : IRequestHandler<GetPricingPlansQuery, IEnumerable<AdvertisementService.Domain.Entities.PricingPlan>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetPricingPlansHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AdvertisementService.Domain.Entities.PricingPlan>> Handle(GetPricingPlansQuery request, CancellationToken cancellationToken)
        {
            return await _repository.PricingPlanRepository.GetAllAsync(cancellationToken);
        }
    }
}

