using System;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.PricingFeature.Command
{
    public class DeletePricingFeatureCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}

