using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.PricingFeature.Command;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.PricingFeature.Handlers
{
    public class CreatePricingFeatureHandler : IRequestHandler<CreatePricingFeatureCommand, Result<string>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public CreatePricingFeatureHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CreatePricingFeatureCommand request, CancellationToken cancellationToken)
        {
            var pricingPlan = await _repository.PricingPlanRepository.GetByIdAsync(request.PricingPlanId);
            if (pricingPlan == null)
            {
                return Result.Fail("PricingPlan not found");
            }

            var pricingFeature = new AdvertisementService.Domain.Entities.PricingFeature
            {
                Id = Guid.NewGuid(),
                PricingPlanId = request.PricingPlanId,
                Description = request.Description,
                IconName = request.IconName,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            await _repository.PricingFeaturesRepository.AddAsync(pricingFeature);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(pricingFeature.Id.ToString());
        }
    }
}

