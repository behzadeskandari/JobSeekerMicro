using System;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.PricingPlan.Command
{
    public class UpdatePricingPlanCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public int? Duration { get; set; }
        public string? DurationUnit { get; set; }
        public int? JobCount { get; set; }
        public int? DiscountPercentage { get; set; }
        public string? ButtonText { get; set; }
        public bool? IsPopular { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }
    }
}

