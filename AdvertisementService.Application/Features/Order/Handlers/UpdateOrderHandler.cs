using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.Order.Command;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Events;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Order.Handlers
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public UpdateOrderHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.OrdersRepository.GetByIdAsync(request.Id);
            if (order == null)
            {
                return Result.Fail("Order not found");
            }

            if (request.PricingPlanId.HasValue)
                order.PricingPlanId = request.PricingPlanId.Value;
            if (!string.IsNullOrEmpty(request.UserId))
                order.UserId = request.UserId;
            if (request.TotalAmount.HasValue)
                order.TotalAmount = request.TotalAmount.Value;
            if (!string.IsNullOrEmpty(request.Status))
                order.Status = request.Status;
            
            order.DateModified = DateTime.UtcNow;

            order.Raise(new OrderPlacedEvent(
                order.Id,
                order.PricingPlanId,
                order.UserId,
                order.TotalAmount));

            await _repository.OrdersRepository.UpdateAsync(order);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

