using System.Threading.Tasks;
using AdvertisementService.Application.Features.PricingCategory.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.PricingCategory.Handlers
{
    public class GetPricingCategoryByIdHandler : IRequestHandler<GetPricingCategoryByIdQuery, AdvertisementService.Domain.Entities.PricingCategory?>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetPricingCategoryByIdHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<AdvertisementService.Domain.Entities.PricingCategory?> Handle(GetPricingCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.PricingCategoryRepository.GetByIdAsync(request.Id);
        }
    }
}

