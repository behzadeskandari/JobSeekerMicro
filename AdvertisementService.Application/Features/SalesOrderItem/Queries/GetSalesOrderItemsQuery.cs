using System.Collections.Generic;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrderItem.Queries
{
    public class GetSalesOrderItemsQuery : IRequest<IEnumerable<AdvertisementService.Domain.Entities.SalesOrderItem>>
    {
    }
}

