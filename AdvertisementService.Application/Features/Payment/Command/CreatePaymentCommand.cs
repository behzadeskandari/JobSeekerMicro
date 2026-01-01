using System;
using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Payment.Command
{
    public class CreatePaymentCommand : IRequest<Result<string>>
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int AdvertisementId { get; set; }

        public Guid? OrderId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string TransactionId { get; set; } = string.Empty;

        public string PaymentMethod { get; set; } = "Zarinpal";
        public string TestType { get; set; } = string.Empty;
    }
}

