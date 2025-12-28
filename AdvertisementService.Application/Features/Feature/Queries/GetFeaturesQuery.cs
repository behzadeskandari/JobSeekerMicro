using System.Collections.Generic;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Feature.Queries
{
    public class GetFeaturesQuery : IRequest<IEnumerable<AdvertisementService.Domain.Entities.Feature>>
    {
    }
}

