using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.Payment.Command;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using AdvertisementService.Domain.Events;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Payment.Handlers
{
    public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, Result<string>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public CreatePaymentHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new AdvertisementService.Domain.Entities.Payment
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                AdvertisementId = request.AdvertisementId,
                OrderId = request.OrderId,
                Amount = request.Amount,
                TransactionId = request.TransactionId,
                PaymentMethod = request.PaymentMethod,
                TestType = request.TestType,
                CreatedAt = DateTime.UtcNow,
                Status = JobSeeker.Shared.Contracts.Enums.PaymentStatus.Pending,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            payment.Raise(new PaymentProcessedEvent(
                payment.Id,
                payment.AdvertisementId,
                payment.UserId,
                payment.Amount,
                payment.Status.ToString()));

            await _repository.PaymentRepository.AddAsync(payment);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(payment.Id.ToString());
        }
    }
}

