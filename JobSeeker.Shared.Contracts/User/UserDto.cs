using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JobSeeker.Shared.Contracts.Job;
using JobSeeker.Shared.Contracts.Payment;
using JobSeeker.Shared.Dtos.Advertisement;

namespace JobSeeker.Shared.Contracts.User
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JWT { get; set; }
        public string Email { get; set; }
        public string? UserName { get; set; }
        public string PictureUrl { get; set; }
        public bool EmailConfirmed { get; set; }

        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string Password { get; set; } = string.Empty;
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;
        public bool? IsActive { get; set; }
        public string Role { get; set; }
        public string RedirectUrl { get; set; }
        [JsonIgnore]
        public ICollection<JobRequestDto> JobRequests { get; set; } = new List<JobRequestDto>();

        [JsonIgnore]
        public ICollection<JobOfferDto> JobOffers { get; set; } = new List<JobOfferDto>();
        [JsonIgnore]
        public ICollection<AdvertisementDto> Advertisements { get; set; } = new List<AdvertisementDto>();
        [JsonIgnore]
        public ICollection<PaymentDto> Payments { get; set; } = new List<PaymentDto>();
    }
}
