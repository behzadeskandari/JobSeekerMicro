using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AdvertisementService.Application.Features.Advertisement.Queries
{
    public class GetAdvertisementsQuery : IRequest<IEnumerable<AdvertisementService.Domain.Entities.Advertisement>>
    {
    }
}
