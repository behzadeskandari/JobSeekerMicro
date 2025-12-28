using System.Threading.Tasks;
using AdvertisementService.Application.Features.ProductInventory.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.ProductInventory.Handlers
{
    public class GetProductInventoryByIdHandler : IRequestHandler<GetProductInventoryByIdQuery, AdvertisementService.Domain.Entities.ProductInventory?>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetProductInventoryByIdHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<AdvertisementService.Domain.Entities.ProductInventory?> Handle(GetProductInventoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ProductInventoryRepository.GetByIdAsync(request.Id);
        }
    }
}

