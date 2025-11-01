using JobSeeker.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementService.Domain.Entities
{
    public class Customer : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int UserId { get; set; }
        //[ForeignKey("CustomerAddress")]
        //public int? PrimaryAddressId { get; set; }
        //public CustomerAddress PrimaryAddress { get; set; }
        [Required]
        [ForeignKey("Order")]
        public Guid? OrdersId { get; set; }
        public Order? Orders { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        // Navigation property for CustomerAddresses
        public ICollection<CustomerAddress> CustomerAddresses { get; set; }
        public ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();
        public string CustomerType { get; set; }
    }
}
