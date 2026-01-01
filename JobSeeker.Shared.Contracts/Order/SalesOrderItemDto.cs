using JobSeeker.Shared.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Contracts.Order
{
    public class SalesOrderItemDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public ProductDto Product { get; set; }
    }


    public class ProductDto
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
        public int CategoryId { get; set; }


        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }

}
