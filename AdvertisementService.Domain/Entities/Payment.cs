using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdvertisementService.Domain.Common;
using JobSeeker.Shared.Contracts.Enums;

namespace AdvertisementService.Domain.Entities
{
    public class Payment : EntityBase<Guid>
    {
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int AdvertisementId { get; set; }

        public Advertisement Advertisement { get; set; } = null!;

        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public string TransactionId { get; set; } = string.Empty;

        public string PaymentMethod { get; set; } = "Zarinpal";
        public string TestType { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    }
}
