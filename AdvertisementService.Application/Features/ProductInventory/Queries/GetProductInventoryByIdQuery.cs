using System;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.ProductInventory.Queries
{
    public class GetProductInventoryByIdQuery : IRequest<AdvertisementService.Domain.Entities.ProductInventory?>
    {
        public Guid Id { get; set; }
    }
}

