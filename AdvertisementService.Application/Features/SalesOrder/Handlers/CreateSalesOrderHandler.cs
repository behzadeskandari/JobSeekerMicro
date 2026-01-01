using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.SalesOrder.Command;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using AdvertisementService.Domain.Events;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrder.Handlers
{
    public class CreateSalesOrderHandler : IRequestHandler<CreateSalesOrderCommand, Result<string>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public CreateSalesOrderHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CreateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.CustomersRepository.GetByIdAsync(request.CustomerId);
            if (customer == null)
            {
                return Result.Fail("Customer not found");
            }

            var salesOrder = new AdvertisementService.Domain.Entities.SalesOrder
            {
                CustomerId = request.CustomerId,
                IsPaid = request.IsPaid,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            salesOrder.Raise(new SalesOrderCreatedEvent(
                salesOrder.Id,
                salesOrder.CustomerId,
                salesOrder.IsPaid));

            await _repository.SalesOrderRepository.AddAsync(salesOrder);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(salesOrder.Id.ToString());
        }
    }
}

