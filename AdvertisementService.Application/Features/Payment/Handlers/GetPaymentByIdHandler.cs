using System.Threading.Tasks;
using AdvertisementService.Application.Features.Payment.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Payment.Handlers
{
    public class GetPaymentByIdHandler : IRequestHandler<GetPaymentByIdQuery, AdvertisementService.Domain.Entities.Payment?>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetPaymentByIdHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<AdvertisementService.Domain.Entities.Payment?> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.PaymentRepository.GetByIdAsync(request.Id);
        }
    }
}

