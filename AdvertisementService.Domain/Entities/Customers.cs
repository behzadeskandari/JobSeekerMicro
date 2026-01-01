using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AdvertisementService.Domain.Common;

namespace AdvertisementService.Domain.Entities
{
    public class Customer : EntityBaseInt
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        
        [ForeignKey("Order")]
        public int? OrdersId { get; set; }
        public Order? Orders { get; set; }
        
        public ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();
        public ICollection<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();
        public string CustomerType { get; set; } = string.Empty;
    }
}
