using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.CustomerAddress.Command
{
    public class CreateCustomerAddressCommand : IRequest<Result<string>>
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string AddressLine1 { get; set; } = string.Empty;

        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
    }
}

