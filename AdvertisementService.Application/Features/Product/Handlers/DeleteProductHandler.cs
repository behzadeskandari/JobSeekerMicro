using System.Threading.Tasks;
using AdvertisementService.Application.Features.Product.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Product.Handlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public DeleteProductHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.ProductRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                return Result.Fail("Product not found");
            }

            await _repository.ProductRepository.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

