using System.Threading.Tasks;
using AdvertisementService.Application.Features.Payment.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Payment.Handlers
{
    public class DeletePaymentHandler : IRequestHandler<DeletePaymentCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public DeletePaymentHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _repository.PaymentRepository.GetByIdAsync(request.Id);
            if (payment == null)
            {
                return Result.Fail("Payment not found");
            }

            await _repository.PaymentRepository.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

