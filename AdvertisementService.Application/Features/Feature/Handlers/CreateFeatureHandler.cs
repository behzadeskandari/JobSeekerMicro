using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.Feature.Command;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Feature.Handlers
{
    public class CreateFeatureHandler : IRequestHandler<CreateFeatureCommand, Result<string>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public CreateFeatureHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CreateFeatureCommand request, CancellationToken cancellationToken)
        {
            var feature = new AdvertisementService.Domain.Entities.Feature
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                IconName = request.IconName,
                Language = request.Language,
                JobsIds = request.JobsIds,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            await _repository.FeatureRepository.AddAsync(feature);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(feature.Id.ToString());
        }
    }
}

