using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.PricingCategory.Queries
{
    public class GetPricingCategoryByIdQuery : IRequest<AdvertisementService.Domain.Entities.PricingCategory?>
    {
        public int Id { get; set; }
    }
}

