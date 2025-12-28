using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.SalesOrderItem.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrderItem.Handlers
{
    public class UpdateSalesOrderItemHandler : IRequestHandler<UpdateSalesOrderItemCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public UpdateSalesOrderItemHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateSalesOrderItemCommand request, CancellationToken cancellationToken)
        {
            var salesOrderItem = await _repository.SalesOrderItemRepository.GetByIdAsync(request.Id);
            if (salesOrderItem == null)
            {
                return Result.Fail("SalesOrderItem not found");
            }

            if (request.Quantity.HasValue)
                salesOrderItem.Quantity = request.Quantity.Value;
            if (request.SalesOrderId.HasValue)
                salesOrderItem.SalesOrderId = request.SalesOrderId;
            
            salesOrderItem.DateModified = DateTime.UtcNow;

            await _repository.SalesOrderItemRepository.UpdateAsync(salesOrderItem);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

