using System;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.PricingPlan.Queries
{
    public class GetPricingPlanByIdQuery : IRequest<AdvertisementService.Domain.Entities.PricingPlan?>
    {
        public Guid Id { get; set; }
    }
}

