using System;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.ProductInventory.Command
{
    public class UpdateProductInventoryCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public int? QuantityOnHand { get; set; }
        public int? IdealQuantity { get; set; }
    }
}

