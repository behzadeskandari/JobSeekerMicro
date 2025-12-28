using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.PricingCategory.Command
{
    public class DeletePricingCategoryCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}

