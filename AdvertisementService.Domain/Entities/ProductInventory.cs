using System.ComponentModel.DataAnnotations.Schema;
using AdvertisementService.Domain.Common;

namespace AdvertisementService.Domain.Entities
{
    public class ProductInventory : EntityBase<Guid>
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int QuantityOnHand { get; set; }
        public int IdealQuantity { get; set; }
        
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
