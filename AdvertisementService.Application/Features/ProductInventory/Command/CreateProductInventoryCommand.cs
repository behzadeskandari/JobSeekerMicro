using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.ProductInventory.Command
{
    public class CreateProductInventoryCommand : IRequest<Result<string>>
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int QuantityOnHand { get; set; }

        [Required]
        public int IdealQuantity { get; set; }
    }
}

