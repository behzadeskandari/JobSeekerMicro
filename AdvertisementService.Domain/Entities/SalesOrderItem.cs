using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdvertisementService.Domain.Common;

namespace AdvertisementService.Domain.Entities
{
    public class SalesOrderItem : EntityBase<Guid>
    {
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        
        [ForeignKey("SalesOrder")]
        public int? SalesOrderId { get; set; }
        public SalesOrder? SalesOrder { get; set; }
    }
}
