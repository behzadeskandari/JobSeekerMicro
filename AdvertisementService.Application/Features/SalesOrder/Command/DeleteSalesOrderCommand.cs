using System;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrder.Command
{
    public class DeleteSalesOrderCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}

