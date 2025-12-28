using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.ProductInventory.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.ProductInventory.Handlers
{
    public class GetProductInventoriesHandler : IRequestHandler<GetProductInventoriesQuery, IEnumerable<AdvertisementService.Domain.Entities.ProductInventory>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetProductInventoriesHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AdvertisementService.Domain.Entities.ProductInventory>> Handle(GetProductInventoriesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ProductInventoryRepository.GetAllAsync(cancellationToken);
        }
    }
}

