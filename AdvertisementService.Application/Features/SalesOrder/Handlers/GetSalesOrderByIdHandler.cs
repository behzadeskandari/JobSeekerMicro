using System.Threading.Tasks;
using AdvertisementService.Application.Features.SalesOrder.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrder.Handlers
{
    public class GetSalesOrderByIdHandler : IRequestHandler<GetSalesOrderByIdQuery, AdvertisementService.Domain.Entities.SalesOrder?>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetSalesOrderByIdHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<AdvertisementService.Domain.Entities.SalesOrder?> Handle(GetSalesOrderByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.SalesOrderRepository.GetByIdAsync(request.Id);
        }
    }
}

