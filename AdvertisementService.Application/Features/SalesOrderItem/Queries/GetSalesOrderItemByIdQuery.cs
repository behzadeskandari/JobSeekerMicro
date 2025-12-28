using System;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrderItem.Queries
{
    public class GetSalesOrderItemByIdQuery : IRequest<AdvertisementService.Domain.Entities.SalesOrderItem?>
    {
        public Guid Id { get; set; }
    }
}

