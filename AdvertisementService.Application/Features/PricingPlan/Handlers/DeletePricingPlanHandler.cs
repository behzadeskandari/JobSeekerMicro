using System.Threading.Tasks;
using AdvertisementService.Application.Features.PricingPlan.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.PricingPlan.Handlers
{
    public class DeletePricingPlanHandler : IRequestHandler<DeletePricingPlanCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public DeletePricingPlanHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeletePricingPlanCommand request, CancellationToken cancellationToken)
        {
            var pricingPlan = await _repository.PricingPlanRepository.GetByIdAsync(request.Id);
            if (pricingPlan == null)
            {
                return Result.Fail("PricingPlan not found");
            }

            await _repository.PricingPlanRepository.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

