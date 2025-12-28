using System;
using System.ComponentModel.DataAnnotations;
using FluentResults;
using JobSeeker.Shared.Contracts.Enums;
using MediatR;

namespace AdvertisementService.Application.Features.Payment.Command
{
    public class UpdatePaymentCommand : IRequest<Result>
    {
        [Required]
        public Guid Id { get; set; }

        public decimal? Amount { get; set; }
        public string? TransactionId { get; set; }
        public string? PaymentMethod { get; set; }
        public PaymentStatus? Status { get; set; }
    }
}

