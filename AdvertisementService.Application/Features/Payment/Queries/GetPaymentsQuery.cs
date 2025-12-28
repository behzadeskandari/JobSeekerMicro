using System.Collections.Generic;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Payment.Queries
{
    public class GetPaymentsQuery : IRequest<IEnumerable<AdvertisementService.Domain.Entities.Payment>>
    {
    }
}

