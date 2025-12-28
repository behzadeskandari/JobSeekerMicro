using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.ProductInventory.Command;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Events;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.ProductInventory.Handlers
{
    public class UpdateProductInventoryHandler : IRequestHandler<UpdateProductInventoryCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public UpdateProductInventoryHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateProductInventoryCommand request, CancellationToken cancellationToken)
        {
            var productInventory = await _repository.ProductInventoryRepository.GetByIdAsync(request.Id);
            if (productInventory == null)
            {
                return Result.Fail("ProductInventory not found");
            }

            if (request.QuantityOnHand.HasValue)
                productInventory.QuantityOnHand = request.QuantityOnHand.Value;
            if (request.IdealQuantity.HasValue)
                productInventory.IdealQuantity = request.IdealQuantity.Value;
            
            productInventory.UpdatedOn = DateTime.UtcNow;
            productInventory.DateModified = DateTime.UtcNow;

            productInventory.Raise(new ProductInventoryUpdatedEvent(
                productInventory.Id,
                productInventory.ProductId,
                productInventory.QuantityOnHand));

            await _repository.ProductInventoryRepository.UpdateAsync(productInventory);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

