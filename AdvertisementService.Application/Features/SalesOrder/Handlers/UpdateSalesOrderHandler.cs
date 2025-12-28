using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.SalesOrder.Command;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Events;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrder.Handlers
{
    public class UpdateSalesOrderHandler : IRequestHandler<UpdateSalesOrderCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public UpdateSalesOrderHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var salesOrder = await _repository.SalesOrderRepository.GetByIdAsync(request.Id);
            if (salesOrder == null)
            {
                return Result.Fail("SalesOrder not found");
            }

            if (request.IsPaid.HasValue)
                salesOrder.IsPaid = request.IsPaid.Value;
            
            salesOrder.UpdatedOn = DateTime.UtcNow;
            salesOrder.DateModified = DateTime.UtcNow;

            salesOrder.Raise(new SalesOrderCreatedEvent(
                salesOrder.Id,
                salesOrder.CustomerId,
                salesOrder.IsPaid));

            await _repository.SalesOrderRepository.UpdateAsync(salesOrder);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

