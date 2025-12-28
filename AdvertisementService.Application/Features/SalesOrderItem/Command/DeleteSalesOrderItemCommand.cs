using System;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrderItem.Command
{
    public class DeleteSalesOrderItemCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}

