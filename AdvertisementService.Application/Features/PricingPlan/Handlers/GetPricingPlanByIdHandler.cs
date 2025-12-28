using System.Threading.Tasks;
using AdvertisementService.Application.Features.PricingPlan.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.PricingPlan.Handlers
{
    public class GetPricingPlanByIdHandler : IRequestHandler<GetPricingPlanByIdQuery, AdvertisementService.Domain.Entities.PricingPlan?>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetPricingPlanByIdHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<AdvertisementService.Domain.Entities.PricingPlan?> Handle(GetPricingPlanByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.PricingPlanRepository.GetByIdAsync(request.Id);
        }
    }
}

