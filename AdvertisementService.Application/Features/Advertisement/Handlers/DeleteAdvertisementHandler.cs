using System.Threading.Tasks;
using AdvertisementService.Application.Features.Advertisement.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Advertisement.Handlers
{
    public class DeleteAdvertisementHandler : IRequestHandler<DeleteAdvertisementCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public DeleteAdvertisementHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteAdvertisementCommand request, CancellationToken cancellationToken)
        {
            var advertisement = await _repository.AdvertisementRepository.GetByIdAsync(request.Id);
            if (advertisement == null)
            {
                return Result.Fail("Advertisement not found");
            }

            await _repository.AdvertisementRepository.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
