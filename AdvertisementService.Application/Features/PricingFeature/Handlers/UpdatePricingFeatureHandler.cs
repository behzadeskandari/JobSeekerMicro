using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.PricingFeature.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.PricingFeature.Handlers
{
    public class UpdatePricingFeatureHandler : IRequestHandler<UpdatePricingFeatureCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public UpdatePricingFeatureHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdatePricingFeatureCommand request, CancellationToken cancellationToken)
        {
            var pricingFeature = await _repository.PricingFeaturesRepository.GetByIdAsync(request.Id);
            if (pricingFeature == null)
            {
                return Result.Fail("PricingFeature not found");
            }

            if (!string.IsNullOrEmpty(request.Description))
                pricingFeature.Description = request.Description;
            if (!string.IsNullOrEmpty(request.IconName))
                pricingFeature.IconName = request.IconName;
            
            pricingFeature.DateModified = DateTime.UtcNow;

            await _repository.PricingFeaturesRepository.UpdateAsync(pricingFeature);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

