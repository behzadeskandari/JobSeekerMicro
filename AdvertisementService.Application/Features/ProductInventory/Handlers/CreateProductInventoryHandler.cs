using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.ProductInventory.Command;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using AdvertisementService.Domain.Events;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.ProductInventory.Handlers
{
    public class CreateProductInventoryHandler : IRequestHandler<CreateProductInventoryCommand, Result<string>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public CreateProductInventoryHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CreateProductInventoryCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.ProductRepository.GetByIdAsync(request.ProductId);
            if (product == null)
            {
                return Result.Fail("Product not found");
            }

            var productInventory = new AdvertisementService.Domain.Entities.ProductInventory
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                QuantityOnHand = request.QuantityOnHand,
                IdealQuantity = request.IdealQuantity,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            productInventory.Raise(new ProductInventoryUpdatedEvent(
                productInventory.Id,
                productInventory.ProductId,
                productInventory.QuantityOnHand));

            await _repository.ProductInventoryRepository.AddAsync(productInventory);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(productInventory.Id.ToString());
        }
    }
}

