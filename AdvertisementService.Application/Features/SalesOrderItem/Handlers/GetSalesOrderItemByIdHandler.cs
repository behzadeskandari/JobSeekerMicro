using System.Threading.Tasks;
using AdvertisementService.Application.Features.SalesOrderItem.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrderItem.Handlers
{
    public class GetSalesOrderItemByIdHandler : IRequestHandler<GetSalesOrderItemByIdQuery, AdvertisementService.Domain.Entities.SalesOrderItem?>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetSalesOrderItemByIdHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<AdvertisementService.Domain.Entities.SalesOrderItem?> Handle(GetSalesOrderItemByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.SalesOrderItemRepository.GetByIdAsync(request.Id);
        }
    }
}

