using System.Threading.Tasks;
using AdvertisementService.Application.Features.Feature.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Feature.Handlers
{
    public class DeleteFeatureHandler : IRequestHandler<DeleteFeatureCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public DeleteFeatureHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteFeatureCommand request, CancellationToken cancellationToken)
        {
            var feature = await _repository.FeatureRepository.GetByIdAsync(request.Id);
            if (feature == null)
            {
                return Result.Fail("Feature not found");
            }

            await _repository.FeatureRepository.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

