using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.Order.Command;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using AdvertisementService.Domain.Events;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Order.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Result<string>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public CreateOrderHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new AdvertisementService.Domain.Entities.Order
            {
                Id = Guid.NewGuid(),
                PricingPlanId = request.PricingPlanId,
                UserId = request.UserId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = request.TotalAmount,
                Status = request.Status,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            order.Raise(new OrderPlacedEvent(
                order.Id,
                order.PricingPlanId,
                order.UserId,
                order.TotalAmount));

            await _repository.OrdersRepository.AddAsync(order);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(order.Id.ToString());
        }
    }
}

