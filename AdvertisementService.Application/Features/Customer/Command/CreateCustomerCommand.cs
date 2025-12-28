using System;
using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Customer.Command
{
    public class CreateCustomerCommand : IRequest<Result<string>>
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty;

        public int? OrdersId { get; set; }
        public string CustomerType { get; set; } = string.Empty;
    }
}

