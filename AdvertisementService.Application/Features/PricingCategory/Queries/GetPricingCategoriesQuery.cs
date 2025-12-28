using System.Collections.Generic;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.PricingCategory.Queries
{
    public class GetPricingCategoriesQuery : IRequest<IEnumerable<AdvertisementService.Domain.Entities.PricingCategory>>
    {
    }
}

