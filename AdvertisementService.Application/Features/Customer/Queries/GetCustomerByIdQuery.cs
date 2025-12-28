using System;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Customer.Queries
{
    public class GetCustomerByIdQuery : IRequest<AdvertisementService.Domain.Entities.Customer?>
    {
        public Guid Id { get; set; }
    }
}

