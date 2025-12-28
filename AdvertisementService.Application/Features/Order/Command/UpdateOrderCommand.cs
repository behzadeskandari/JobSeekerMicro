using System;
using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Order.Command
{
    public class UpdateOrderCommand : IRequest<Result>
    {
        [Required]
        public Guid Id { get; set; }

        public int? PricingPlanId { get; set; }
        public string? UserId { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? Status { get; set; }
    }
}

