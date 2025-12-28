using System.Collections.Generic;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrder.Queries
{
    public class GetSalesOrdersQuery : IRequest<IEnumerable<AdvertisementService.Domain.Entities.SalesOrder>>
    {
    }
}

