using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.PricingFeature.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.PricingFeature.Handlers
{
    public class GetPricingFeaturesHandler : IRequestHandler<GetPricingFeaturesQuery, IEnumerable<AdvertisementService.Domain.Entities.PricingFeature>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetPricingFeaturesHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AdvertisementService.Domain.Entities.PricingFeature>> Handle(GetPricingFeaturesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.PricingFeaturesRepository.GetAllAsync(cancellationToken);
        }
    }
}

