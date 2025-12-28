using System;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Customer.Command
{
    public class UpdateCustomerCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CustomerType { get; set; }
    }
}

