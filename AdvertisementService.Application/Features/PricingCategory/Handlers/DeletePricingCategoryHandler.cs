using System.Threading.Tasks;
using AdvertisementService.Application.Features.PricingCategory.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.PricingCategory.Handlers
{
    public class DeletePricingCategoryHandler : IRequestHandler<DeletePricingCategoryCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public DeletePricingCategoryHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeletePricingCategoryCommand request, CancellationToken cancellationToken)
        {
            var pricingCategory = await _repository.PricingCategoryRepository.GetByIdAsync(request.Id);
            if (pricingCategory == null)
            {
                return Result.Fail("PricingCategory not found");
            }

            await _repository.PricingCategoryRepository.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

