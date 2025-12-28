using System;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Order.Queries
{
    public class GetOrderByIdQuery : IRequest<AdvertisementService.Domain.Entities.Order?>
    {
        public Guid Id { get; set; }
    }
}

