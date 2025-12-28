using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.Order.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Order.Handlers
{
    public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, IEnumerable<AdvertisementService.Domain.Entities.Order>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetOrdersHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AdvertisementService.Domain.Entities.Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _repository.OrdersRepository.GetAllAsync(cancellationToken);
        }
    }
}

