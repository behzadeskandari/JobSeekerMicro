using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Contracts.Enums;
using JobSeeker.Shared.Kernel.Abstractions;
using JobSeeker.Shared.Kernel.Domain;
using JobSeeker.Shared.Models;

namespace AdvertisementService.Domain.Entities
{
    public class Product : IBaseEntity<Guid>, IAggregateRoot
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        [MaxLength(128)]
        public string Description { get; set; }
        public string sku { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }
        [Required]
        public bool IsTaxable { get; set; }
        [Required]
        public bool IsArchived { get; set; }
        public ProductType type { get; set; }

        public ProductStatus status { get; set; }
        [Required]
        public int TaxRate { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public string Dimensions { get; set; }
        public string FeaturedImageUrl { get; set; }
        [Required]
        public string[] GalleryImageUrls { get; set; }
        [Required]
        public string[] Tags { get; set; }
        [Required]
        public string[] Attributes { get; set; }
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        //[NotMapped]
        public Category Category { get; set; }


        public ICollection<SalesOrderItem> SalesOrderItems { get; set; } = new List<SalesOrderItem>();
        public ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();
        public ICollection<ProductInventorySnapshot> ProductInventorySnapshots { get; set; } = new List<ProductInventorySnapshot>();


        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }


        private readonly List<DomainEvent> _domainEvents = new();

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
