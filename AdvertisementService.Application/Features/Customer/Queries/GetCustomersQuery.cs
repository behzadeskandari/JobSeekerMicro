using System.Collections.Generic;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Customer.Queries
{
    public class GetCustomersQuery : IRequest<IEnumerable<AdvertisementService.Domain.Entities.Customer>>
    {
    }
}

