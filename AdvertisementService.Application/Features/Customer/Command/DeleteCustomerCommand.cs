using System;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Customer.Command
{
    public class DeleteCustomerCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}

