using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.PricingPlan.Command
{
    public class CreatePricingPlanCommand : IRequest<Result<string>>
    {
        [Required]
        public int PricingCategoryId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Subtitle { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        public string Currency { get; set; } = "USD";
        public int Duration { get; set; }
        public string DurationUnit { get; set; } = "month";
        public int JobCount { get; set; }
        public int? DiscountPercentage { get; set; }
        public string ButtonText { get; set; } = string.Empty;
        public bool? IsPopular { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}

