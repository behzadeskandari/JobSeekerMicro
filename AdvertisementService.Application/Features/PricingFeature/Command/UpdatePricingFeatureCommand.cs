using System;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.PricingFeature.Command
{
    public class UpdatePricingFeatureCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string? IconName { get; set; }
    }
}

