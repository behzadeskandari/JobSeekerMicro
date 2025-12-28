using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.Feature.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Feature.Handlers
{
    public class UpdateFeatureHandler : IRequestHandler<UpdateFeatureCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public UpdateFeatureHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateFeatureCommand request, CancellationToken cancellationToken)
        {
            var feature = await _repository.FeatureRepository.GetByIdAsync(request.Id);
            if (feature == null)
            {
                return Result.Fail("Feature not found");
            }

            if (!string.IsNullOrEmpty(request.Title))
                feature.Title = request.Title;
            if (!string.IsNullOrEmpty(request.Description))
                feature.Description = request.Description;
            if (!string.IsNullOrEmpty(request.IconName))
                feature.IconName = request.IconName;
            if (!string.IsNullOrEmpty(request.Language))
                feature.Language = request.Language;
            if (request.JobsIds != null)
                feature.JobsIds = request.JobsIds;
            
            feature.DateModified = DateTime.UtcNow;

            await _repository.FeatureRepository.UpdateAsync(feature);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

