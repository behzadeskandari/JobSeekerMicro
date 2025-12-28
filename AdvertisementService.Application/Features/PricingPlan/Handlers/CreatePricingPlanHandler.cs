using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.PricingPlan.Command;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.PricingPlan.Handlers
{
    public class CreatePricingPlanHandler : IRequestHandler<CreatePricingPlanCommand, Result<string>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public CreatePricingPlanHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CreatePricingPlanCommand request, CancellationToken cancellationToken)
        {
            var pricingCategory = await _repository.PricingCategoryRepository.GetByIdAsync(request.PricingCategoryId);
            if (pricingCategory == null)
            {
                return Result.Fail("PricingCategory not found");
            }

            var pricingPlan = new AdvertisementService.Domain.Entities.PricingPlan
            {
                Id = Guid.NewGuid(),
                PricingCategoryId = request.PricingCategoryId,
                Title = request.Title,
                Subtitle = request.Subtitle,
                Price = request.Price,
                Currency = request.Currency,
                Duration = request.Duration,
                DurationUnit = request.DurationUnit,
                JobCount = request.JobCount,
                DiscountPercentage = request.DiscountPercentage,
                ButtonText = request.ButtonText,
                IsPopular = request.IsPopular,
                Type = request.Type,
                Name = request.Name,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            await _repository.PricingPlanRepository.AddAsync(pricingPlan);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(pricingPlan.Id.ToString());
        }
    }
}

