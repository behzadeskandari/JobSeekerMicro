using System;
using FluentResults;
using JobSeeker.Shared.Contracts.Enums;
using MediatR;

namespace AdvertisementService.Application.Features.Product.Command
{
    public class UpdateProductCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? SalePrice { get; set; }
        public ProductStatus? Status { get; set; }
        public int? CategoryId { get; set; }
    }
}

