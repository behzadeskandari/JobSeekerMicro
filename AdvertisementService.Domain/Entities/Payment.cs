using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Contracts.Enums;
using JobSeeker.Shared.Models;

namespace AdvertisementService.Domain.Entities
{
    public class Payment : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }

        //[Required]
        public string UserId { get; set; }

        //public User User { get; set; }

        [Required]
        public Guid AdvertisementId { get; set; }

        public Advertisement Advertisement { get; set; }


        public Guid? OrderId { get; set; }
        public Order Order { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public string TransactionId { get; set; }

        public string PaymentMethod { get; set; } = "Zarinpal";
        public string TestType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }
}
