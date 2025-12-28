using System;
using System.ComponentModel.DataAnnotations;
using FluentResults;
using JobSeeker.Shared.Contracts.Enums;
using MediatR;

namespace AdvertisementService.Application.Features.Product.Command
{
    public class CreateProductCommand : IRequest<Result<string>>
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(128)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Sku { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        public decimal SalePrice { get; set; }

        [Required]
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
        public int CategoryId { get; set; }
    }
}

