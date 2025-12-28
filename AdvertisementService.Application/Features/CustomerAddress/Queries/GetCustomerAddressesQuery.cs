using System.Collections.Generic;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.CustomerAddress.Queries
{
    public class GetCustomerAddressesQuery : IRequest<IEnumerable<AdvertisementService.Domain.Entities.CustomerAddress>>
    {
    }
}

