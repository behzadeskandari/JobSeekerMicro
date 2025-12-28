using System.Collections.Generic;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.PricingFeature.Queries
{
    public class GetPricingFeaturesQuery : IRequest<IEnumerable<AdvertisementService.Domain.Entities.PricingFeature>>
    {
    }
}

