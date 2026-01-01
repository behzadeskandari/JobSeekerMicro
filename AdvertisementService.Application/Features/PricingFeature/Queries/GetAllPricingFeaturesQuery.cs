using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementService.Application.Features.PricingFeature.Queries
{
    public record GetAllPricingFeaturesQuery : IRequest<List<AdvertisementService.Domain.Entities.PricingFeature>>;
}
