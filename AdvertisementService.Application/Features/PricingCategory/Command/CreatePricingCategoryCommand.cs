using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.PricingCategory.Command
{
    public class CreatePricingCategoryCommand : IRequest<Result<int>>
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public string IconName { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
    }
}

