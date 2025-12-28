using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.SalesOrderItem.Command;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrderItem.Handlers
{
    public class CreateSalesOrderItemHandler : IRequestHandler<CreateSalesOrderItemCommand, Result<string>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public CreateSalesOrderItemHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CreateSalesOrderItemCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.ProductRepository.GetByIdAsync(request.ProductId);
            if (product == null)
            {
                return Result.Fail("Product not found");
            }

            if (request.SalesOrderId.HasValue)
            {
                var salesOrder = await _repository.SalesOrderRepository.GetByIdAsync(request.SalesOrderId.Value);
                if (salesOrder == null)
                {
                    return Result.Fail("SalesOrder not found");
                }
            }

            var salesOrderItem = new AdvertisementService.Domain.Entities.SalesOrderItem
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                SalesOrderId = request.SalesOrderId,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            await _repository.SalesOrderItemRepository.AddAsync(salesOrderItem);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(salesOrderItem.Id.ToString());
        }
    }
}

