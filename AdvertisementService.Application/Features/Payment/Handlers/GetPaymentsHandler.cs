using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.Payment.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Payment.Handlers
{
    public class GetPaymentsHandler : IRequestHandler<GetPaymentsQuery, IEnumerable<AdvertisementService.Domain.Entities.Payment>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetPaymentsHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AdvertisementService.Domain.Entities.Payment>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.PaymentRepository.GetAllAsync(cancellationToken);
        }
    }
}

