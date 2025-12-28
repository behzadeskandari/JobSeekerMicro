using System;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Product.Queries
{
    public class GetProductByIdQuery : IRequest<AdvertisementService.Domain.Entities.Product?>
    {
        public Guid Id { get; set; }
    }
}

