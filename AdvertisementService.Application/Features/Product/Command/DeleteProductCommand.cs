using System;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Product.Command
{
    public class DeleteProductCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}

