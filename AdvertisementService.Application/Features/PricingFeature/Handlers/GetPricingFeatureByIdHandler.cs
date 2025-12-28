using System.Threading.Tasks;
using AdvertisementService.Application.Features.PricingFeature.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.PricingFeature.Handlers
{
    public class GetPricingFeatureByIdHandler : IRequestHandler<GetPricingFeatureByIdQuery, AdvertisementService.Domain.Entities.PricingFeature?>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetPricingFeatureByIdHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<AdvertisementService.Domain.Entities.PricingFeature?> Handle(GetPricingFeatureByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.PricingFeaturesRepository.GetByIdAsync(request.Id);
        }
    }
}

