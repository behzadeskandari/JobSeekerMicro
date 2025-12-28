using System.Threading.Tasks;
using AdvertisementService.Application.Features.Order.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Order.Handlers
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, AdvertisementService.Domain.Entities.Order?>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetOrderByIdHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<AdvertisementService.Domain.Entities.Order?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.OrdersRepository.GetByIdAsync(request.Id);
        }
    }
}

