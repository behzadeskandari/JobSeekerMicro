using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdvertisementService.Domain.Common;

namespace AdvertisementService.Domain.Entities
{
    public class SalesOrder : EntityBase<Guid>
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        
        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        
        public List<SalesOrderItem> SalesOrderItems { get; set; } = new List<SalesOrderItem>();
        public bool IsPaid { get; set; }
    }
}
