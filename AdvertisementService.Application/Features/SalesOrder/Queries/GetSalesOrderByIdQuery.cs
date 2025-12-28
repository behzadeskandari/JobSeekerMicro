using System;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrder.Queries
{
    public class GetSalesOrderByIdQuery : IRequest<AdvertisementService.Domain.Entities.SalesOrder?>
    {
        public Guid Id { get; set; }
    }
}

