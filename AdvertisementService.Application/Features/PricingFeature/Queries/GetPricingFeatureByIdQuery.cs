using System;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.PricingFeature.Queries
{
    public class GetPricingFeatureByIdQuery : IRequest<AdvertisementService.Domain.Entities.PricingFeature?>
    {
        public Guid Id { get; set; }
    }
}

