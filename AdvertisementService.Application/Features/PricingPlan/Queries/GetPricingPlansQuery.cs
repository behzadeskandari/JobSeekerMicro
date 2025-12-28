using System.Collections.Generic;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.PricingPlan.Queries
{
    public class GetPricingPlansQuery : IRequest<IEnumerable<AdvertisementService.Domain.Entities.PricingPlan>>
    {
    }
}

