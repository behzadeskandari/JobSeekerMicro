using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.PricingCategory.Command
{
    public class UpdatePricingCategoryCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? IconName { get; set; }
        public string? Language { get; set; }
    }
}

