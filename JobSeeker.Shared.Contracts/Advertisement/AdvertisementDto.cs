using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Contracts.Category;
using JobSeeker.Shared.Contracts.Company;
using JobSeeker.Shared.Contracts.Payment;
using JobSeeker.Shared.Contracts.User;

namespace JobSeeker.Shared.Contracts.Advertisement
{
    public class AdvertisementDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string StaffId { get; set; }

        public UserDto? Staff { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public CategoryDto? Category { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public CompanyGetDto? Company { get; set; }

        public DateTime JobADVCreatedAt { get; set; } = DateTime.Now;

        public DateTime? ExpiresAt { get; set; }

        public bool IsApproved { get; set; } = false;

        public bool IsPaid { get; set; } = false;

        public PaymentDto? Payment { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        public string? StaffEmail { get; set; }
        public string? CategoryName { get; set; }
    }
}
