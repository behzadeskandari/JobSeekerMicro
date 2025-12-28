using System.Threading.Tasks;
using AdvertisementService.Application.Features.Feature.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Feature.Handlers
{
    public class GetFeatureByIdHandler : IRequestHandler<GetFeatureByIdQuery, AdvertisementService.Domain.Entities.Feature?>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetFeatureByIdHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<AdvertisementService.Domain.Entities.Feature?> Handle(GetFeatureByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.FeatureRepository.GetByIdAsync(request.Id);
        }
    }
}

