using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.SalesOrderItem.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrderItem.Handlers
{
    public class GetSalesOrderItemsHandler : IRequestHandler<GetSalesOrderItemsQuery, IEnumerable<AdvertisementService.Domain.Entities.SalesOrderItem>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetSalesOrderItemsHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AdvertisementService.Domain.Entities.SalesOrderItem>> Handle(GetSalesOrderItemsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.SalesOrderItemRepository.GetAllAsync(cancellationToken);
        }
    }
}

