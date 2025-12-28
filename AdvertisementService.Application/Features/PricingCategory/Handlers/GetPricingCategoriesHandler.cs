using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.PricingCategory.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.PricingCategory.Handlers
{
    public class GetPricingCategoriesHandler : IRequestHandler<GetPricingCategoriesQuery, IEnumerable<AdvertisementService.Domain.Entities.PricingCategory>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetPricingCategoriesHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AdvertisementService.Domain.Entities.PricingCategory>> Handle(GetPricingCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.PricingCategoryRepository.GetAllAsync(cancellationToken);
        }
    }
}

