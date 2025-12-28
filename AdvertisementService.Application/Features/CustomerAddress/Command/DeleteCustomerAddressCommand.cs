using System;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.CustomerAddress.Command
{
    public class DeleteCustomerAddressCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}

