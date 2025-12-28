using System;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.ProductInventory.Command
{
    public class DeleteProductInventoryCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}

