using System;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.PricingPlan.Command
{
    public class DeletePricingPlanCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}

