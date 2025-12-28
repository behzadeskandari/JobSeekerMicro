using System;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.CustomerAddress.Queries
{
    public class GetCustomerAddressByIdQuery : IRequest<AdvertisementService.Domain.Entities.CustomerAddress?>
    {
        public Guid Id { get; set; }
    }
}

