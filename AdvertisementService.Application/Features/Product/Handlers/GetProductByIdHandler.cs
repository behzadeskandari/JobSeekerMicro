using System.Threading.Tasks;
using AdvertisementService.Application.Features.Product.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Product.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, AdvertisementService.Domain.Entities.Product?>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetProductByIdHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<AdvertisementService.Domain.Entities.Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ProductRepository.GetByIdAsync(request.Id);
        }
    }
}

