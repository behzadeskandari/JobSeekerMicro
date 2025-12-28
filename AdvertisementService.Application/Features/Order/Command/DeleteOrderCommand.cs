using System;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Order.Command
{
    public class DeleteOrderCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}

