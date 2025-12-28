using System.ComponentModel.DataAnnotations.Schema;
using AdvertisementService.Domain.Common;

namespace AdvertisementService.Domain.Entities
{
    public class ProductInventorySnapshot : EntityBase<Guid>
    {
        public DateTime SnapshotTime { get; set; }
        public int QuantityOnHand { get; set; }
        
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
