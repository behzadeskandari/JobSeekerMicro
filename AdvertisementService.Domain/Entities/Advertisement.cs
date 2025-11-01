using JobSeeker.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementService.Domain.Entities
{
    public class Advertisement : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string UserId { get; set; }

        //public User Staff { get; set; }

        [Required]
        public int CategoryId { get; set; }

        //public Category Category { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
        //public Company Company { get; set; }

        public DateTime JobADVCreatedAt { get; set; } = DateTime.Now;

        public DateTime? ExpiresAt { get; set; }

        public bool IsApproved { get; set; } = false;

        public bool IsPaid { get; set; } = false;

        public Guid? PaymentId { get; set; }
        //public Payment? Payment { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }
}
