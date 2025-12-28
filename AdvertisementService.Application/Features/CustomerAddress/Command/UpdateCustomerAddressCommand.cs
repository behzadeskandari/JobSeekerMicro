using System;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.CustomerAddress.Command
{
    public class UpdateCustomerAddressCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
    }
}

