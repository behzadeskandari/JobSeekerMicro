using System;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrder.Command
{
    public class UpdateSalesOrderCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public bool? IsPaid { get; set; }
    }
}

