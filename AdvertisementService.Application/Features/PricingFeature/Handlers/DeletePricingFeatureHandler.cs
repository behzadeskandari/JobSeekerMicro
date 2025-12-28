using System.Threading.Tasks;
using AdvertisementService.Application.Features.PricingFeature.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.PricingFeature.Handlers
{
    public class DeletePricingFeatureHandler : IRequestHandler<DeletePricingFeatureCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public DeletePricingFeatureHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeletePricingFeatureCommand request, CancellationToken cancellationToken)
        {
            var pricingFeature = await _repository.PricingFeaturesRepository.GetByIdAsync(request.Id);
            if (pricingFeature == null)
            {
                return Result.Fail("PricingFeature not found");
            }

            await _repository.PricingFeaturesRepository.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

