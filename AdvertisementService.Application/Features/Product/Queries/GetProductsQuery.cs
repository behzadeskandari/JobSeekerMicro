using System.Collections.Generic;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Product.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<AdvertisementService.Domain.Entities.Product>>
    {
    }
}

