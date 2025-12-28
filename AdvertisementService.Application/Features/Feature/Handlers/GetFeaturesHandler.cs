using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.Feature.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Feature.Handlers
{
    public class GetFeaturesHandler : IRequestHandler<GetFeaturesQuery, IEnumerable<AdvertisementService.Domain.Entities.Feature>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetFeaturesHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AdvertisementService.Domain.Entities.Feature>> Handle(GetFeaturesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.FeatureRepository.GetAllAsync(cancellationToken);
        }
    }
}

