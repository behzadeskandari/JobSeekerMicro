using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.PricingCategory.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.PricingCategory.Handlers
{
    public class UpdatePricingCategoryHandler : IRequestHandler<UpdatePricingCategoryCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public UpdatePricingCategoryHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdatePricingCategoryCommand request, CancellationToken cancellationToken)
        {
            var pricingCategory = await _repository.PricingCategoryRepository.GetByIdAsync(request.Id);
            if (pricingCategory == null)
            {
                return Result.Fail("PricingCategory not found");
            }

            if (!string.IsNullOrEmpty(request.Name))
                pricingCategory.Name = request.Name;
            if (!string.IsNullOrEmpty(request.Description))
                pricingCategory.Description = request.Description;
            if (!string.IsNullOrEmpty(request.IconName))
                pricingCategory.IconName = request.IconName;
            if (!string.IsNullOrEmpty(request.Language))
                pricingCategory.Language = request.Language;
            
            pricingCategory.DateModified = DateTime.UtcNow;

            await _repository.PricingCategoryRepository.UpdateAsync(pricingCategory);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

