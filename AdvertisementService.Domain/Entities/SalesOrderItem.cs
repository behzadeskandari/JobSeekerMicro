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
    public class SalesOrderItem : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        [ForeignKey("SalesOrder")]
        public Guid? SalesOrderId { get; set; }
        public SalesOrder SalesOrder { get; set; }
    }
}
