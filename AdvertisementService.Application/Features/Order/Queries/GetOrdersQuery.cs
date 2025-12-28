using System.Collections.Generic;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Order.Queries
{
    public class GetOrdersQuery : IRequest<IEnumerable<AdvertisementService.Domain.Entities.Order>>
    {
    }
}

