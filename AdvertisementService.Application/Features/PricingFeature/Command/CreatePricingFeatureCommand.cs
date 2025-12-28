using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.PricingFeature.Command
{
    public class CreatePricingFeatureCommand : IRequest<Result<string>>
    {
        [Required]
        public int PricingPlanId { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string IconName { get; set; } = string.Empty;
    }
}

