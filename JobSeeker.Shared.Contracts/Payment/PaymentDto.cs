using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Contracts.Enums;

namespace JobSeeker.Shared.Contracts.Payment
{
    public class PaymentDto
    {
        public int Id { get; set; }

        public string StaffId { get; set; }

        public string StaffEmail { get; set; }

        public int AdvertisementId { get; set; }

        public string AdvertisementTitle { get; set; }

        public decimal Amount { get; set; }

        public string TransactionId { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime CreatedAt { get; set; }

        public PaymentStatus Status { get; set; }

        public string StatusString => Status.ToString();

    }
}
