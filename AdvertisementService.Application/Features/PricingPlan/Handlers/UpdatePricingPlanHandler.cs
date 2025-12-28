using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.PricingPlan.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.PricingPlan.Handlers
{
    public class UpdatePricingPlanHandler : IRequestHandler<UpdatePricingPlanCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public UpdatePricingPlanHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdatePricingPlanCommand request, CancellationToken cancellationToken)
        {
            var pricingPlan = await _repository.PricingPlanRepository.GetByIdAsync(request.Id);
            if (pricingPlan == null)
            {
                return Result.Fail("PricingPlan not found");
            }

            if (!string.IsNullOrEmpty(request.Title))
                pricingPlan.Title = request.Title;
            if (!string.IsNullOrEmpty(request.Subtitle))
                pricingPlan.Subtitle = request.Subtitle;
            if (request.Price.HasValue)
                pricingPlan.Price = request.Price.Value;
            if (!string.IsNullOrEmpty(request.Currency))
                pricingPlan.Currency = request.Currency;
            if (request.Duration.HasValue)
                pricingPlan.Duration = request.Duration.Value;
            if (!string.IsNullOrEmpty(request.DurationUnit))
                pricingPlan.DurationUnit = request.DurationUnit;
            if (request.JobCount.HasValue)
                pricingPlan.JobCount = request.JobCount.Value;
            if (request.DiscountPercentage.HasValue)
                pricingPlan.DiscountPercentage = request.DiscountPercentage;
            if (!string.IsNullOrEmpty(request.ButtonText))
                pricingPlan.ButtonText = request.ButtonText;
            if (request.IsPopular.HasValue)
                pricingPlan.IsPopular = request.IsPopular;
            if (!string.IsNullOrEmpty(request.Type))
                pricingPlan.Type = request.Type;
            if (!string.IsNullOrEmpty(request.Name))
                pricingPlan.Name = request.Name;
            
            pricingPlan.DateModified = DateTime.UtcNow;

            await _repository.PricingPlanRepository.UpdateAsync(pricingPlan);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

