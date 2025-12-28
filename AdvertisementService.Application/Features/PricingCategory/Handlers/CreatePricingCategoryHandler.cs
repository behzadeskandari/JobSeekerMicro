using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.PricingCategory.Command;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.PricingCategory.Handlers
{
    public class CreatePricingCategoryHandler : IRequestHandler<CreatePricingCategoryCommand, Result<int>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public CreatePricingCategoryHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CreatePricingCategoryCommand request, CancellationToken cancellationToken)
        {
            var pricingCategory = new AdvertisementService.Domain.Entities.PricingCategory
            {
                Name = request.Name,
                Description = request.Description,
                IconName = request.IconName,
                Language = request.Language,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            await _repository.PricingCategoryRepository.AddAsync(pricingCategory);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(pricingCategory.Id);
        }
    }
}

