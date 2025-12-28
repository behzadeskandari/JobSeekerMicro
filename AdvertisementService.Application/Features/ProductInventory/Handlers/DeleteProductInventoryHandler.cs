using System.Threading.Tasks;
using AdvertisementService.Application.Features.ProductInventory.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.ProductInventory.Handlers
{
    public class DeleteProductInventoryHandler : IRequestHandler<DeleteProductInventoryCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public DeleteProductInventoryHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteProductInventoryCommand request, CancellationToken cancellationToken)
        {
            var productInventory = await _repository.ProductInventoryRepository.GetByIdAsync(request.Id);
            if (productInventory == null)
            {
                return Result.Fail("ProductInventory not found");
            }

            await _repository.ProductInventoryRepository.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

