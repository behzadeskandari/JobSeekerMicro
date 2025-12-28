using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdvertisementService.Domain.Common;

namespace AdvertisementService.Domain.Entities
{
    public class CustomerAddress : EntityBase<Guid>
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        [MaxLength(100)]
        public string AddressLine1 { get; set; } = string.Empty;

        [MaxLength(100)]
        public string AddressLine2 { get; set; } = string.Empty;

        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [MaxLength(2)]
        public string State { get; set; } = string.Empty;

        [MaxLength(10)]
        public string PostalCode { get; set; } = string.Empty;

        [MaxLength(32)]
        public string Country { get; set; } = string.Empty;
        
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public string Street { get; set; } = string.Empty;
    }
}
