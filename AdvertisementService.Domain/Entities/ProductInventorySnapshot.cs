using JobSeeker.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementService.Domain.Entities
{
    public class ProductInventorySnapshot : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime SnapshotTime { get; set; }
        public int QuantityOnHand { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }
}
