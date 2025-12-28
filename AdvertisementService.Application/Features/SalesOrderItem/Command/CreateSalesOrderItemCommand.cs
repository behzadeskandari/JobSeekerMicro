using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrderItem.Command
{
    public class CreateSalesOrderItemCommand : IRequest<Result<string>>
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int? SalesOrderId { get; set; }
    }
}

