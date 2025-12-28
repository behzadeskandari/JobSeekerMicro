using System;
using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Order.Command
{
    public class CreateOrderCommand : IRequest<Result<string>>
    {
        [Required]
        public int PricingPlanId { get; set; }

        public string? UserId { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Pending";
    }
}

