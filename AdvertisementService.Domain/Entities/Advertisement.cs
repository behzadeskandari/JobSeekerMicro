using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdvertisementService.Domain.Common;
using JobSeeker.Shared.Kernel.Domain;

namespace AdvertisementService.Domain.Entities
{
    public class Advertisement : EntityBase<Guid>
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        public DateTime JobADVCreatedAt { get; set; } = DateTime.Now;

        public DateTime? ExpiresAt { get; set; }

        public bool IsApproved { get; set; } = false;

        public bool IsPaid { get; set; } = false;

        public int? PaymentId { get; set; }

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
