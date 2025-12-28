using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.Payment.Command;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Events;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Payment.Handlers
{
    public class UpdatePaymentHandler : IRequestHandler<UpdatePaymentCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public UpdatePaymentHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _repository.PaymentRepository.GetByIdAsync(request.Id);
            if (payment == null)
            {
                return Result.Fail("Payment not found");
            }

            if (request.Amount.HasValue)
                payment.Amount = request.Amount.Value;
            if (!string.IsNullOrEmpty(request.TransactionId))
                payment.TransactionId = request.TransactionId;
            if (!string.IsNullOrEmpty(request.PaymentMethod))
                payment.PaymentMethod = request.PaymentMethod;
            if (request.Status.HasValue)
                payment.Status = request.Status.Value;
            
            payment.DateModified = DateTime.UtcNow;

            payment.Raise(new PaymentProcessedEvent(
                payment.Id,
                payment.AdvertisementId,
                payment.UserId,
                payment.Amount,
                payment.Status.ToString()));

            await _repository.PaymentRepository.UpdateAsync(payment);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

