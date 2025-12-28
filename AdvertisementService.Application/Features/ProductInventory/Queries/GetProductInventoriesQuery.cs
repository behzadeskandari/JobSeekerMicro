using System.Collections.Generic;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.ProductInventory.Queries
{
    public class GetProductInventoriesQuery : IRequest<IEnumerable<AdvertisementService.Domain.Entities.ProductInventory>>
    {
    }
}

