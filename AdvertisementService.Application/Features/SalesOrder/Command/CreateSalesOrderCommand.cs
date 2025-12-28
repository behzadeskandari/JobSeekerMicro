using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrder.Command
{
    public class CreateSalesOrderCommand : IRequest<Result<string>>
    {
        [Required]
        public int CustomerId { get; set; }

        public bool IsPaid { get; set; } = false;
    }
}

