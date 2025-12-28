using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.Product.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Product.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public UpdateProductHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.ProductRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                return Result.Fail("Product not found");
            }

            if (!string.IsNullOrEmpty(request.Name))
                product.Name = request.Name;
            if (!string.IsNullOrEmpty(request.Description))
                product.Description = request.Description;
            if (request.Price.HasValue)
                product.Price = request.Price.Value;
            if (request.SalePrice.HasValue)
                product.SalePrice = request.SalePrice.Value;
            if (request.Status.HasValue)
                product.Status = request.Status.Value;
            if (request.CategoryId.HasValue)
                product.CategoryId = request.CategoryId.Value;
            
            product.UpdatedOn = DateTime.UtcNow;
            product.DateModified = DateTime.UtcNow;

            await _repository.ProductRepository.UpdateAsync(product);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

