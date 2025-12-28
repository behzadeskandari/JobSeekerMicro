using System;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Feature.Queries
{
    public class GetFeatureByIdQuery : IRequest<AdvertisementService.Domain.Entities.Feature?>
    {
        public Guid Id { get; set; }
    }
}

