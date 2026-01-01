using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdvertisementService.Domain.Common;
using JobSeeker.Shared.Contracts.Enums;

namespace AdvertisementService.Domain.Entities
{
    public class Product : EntityBaseInt
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(128)]
        public string Description { get; set; } = string.Empty;
        
        public string Sku { get; set; } = string.Empty;
        
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
        
        public ProductType Type { get; set; }
        public ProductStatus Status { get; set; }
        
        [Required]
        public int TaxRate { get; set; }
        
        [Required]
        public int Weight { get; set; }
        
        [Required]
        public string Dimensions { get; set; } = string.Empty;
        
        public string FeaturedImageUrl { get; set; } = string.Empty;
        
        [Required]
        public string[] GalleryImageUrls { get; set; } = Array.Empty<string>();
        
        [Required]
        public string[] Tags { get; set; } = Array.Empty<string>();
        
        [Required]
        public string[] Attributes { get; set; } = Array.Empty<string>();
        
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public PricingCategory Category { get; set; } = null!;

        public ICollection<SalesOrderItem> SalesOrderItems { get; set; } = new List<SalesOrderItem>();
        public ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();
        public ICollection<ProductInventorySnapshot> ProductInventorySnapshots { get; set; } = new List<ProductInventorySnapshot>();
    }
}
