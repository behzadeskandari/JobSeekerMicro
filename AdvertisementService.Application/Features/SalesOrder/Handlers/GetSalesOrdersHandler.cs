using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.SalesOrder.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrder.Handlers
{
    public class GetSalesOrdersHandler : IRequestHandler<GetSalesOrdersQuery, IEnumerable<AdvertisementService.Domain.Entities.SalesOrder>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetSalesOrdersHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AdvertisementService.Domain.Entities.SalesOrder>> Handle(GetSalesOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _repository.SalesOrderRepository.GetAllAsync(cancellationToken);
        }
    }
}

