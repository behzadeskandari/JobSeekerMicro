using System;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrderItem.Command
{
    public class UpdateSalesOrderItemCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public int? Quantity { get; set; }
        public int? SalesOrderId { get; set; }
    }
}

