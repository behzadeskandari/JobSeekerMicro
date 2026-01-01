using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.Product.Command;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Product.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Result<string>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public CreateProductHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.PricingCategoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                return Result.Fail("Category not found");
            }

            var product = new AdvertisementService.Domain.Entities.Product
            {
                Name = request.Name,
                Description = request.Description,
                Sku = request.Sku,
                Price = request.Price,
                SalePrice = request.SalePrice,
                Cost = request.Cost,
                IsTaxable = request.IsTaxable,
                IsArchived = request.IsArchived,
                Type = request.Type,
                Status = request.Status,
                TaxRate = request.TaxRate,
                Weight = request.Weight,
                Dimensions = request.Dimensions,
                FeaturedImageUrl = request.FeaturedImageUrl,
                GalleryImageUrls = request.GalleryImageUrls,
                Tags = request.Tags,
                Attributes = request.Attributes,
                CategoryId = request.CategoryId,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            await _repository.ProductRepository.AddAsync(product);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(product.Id.ToString());
        }
    }
}

