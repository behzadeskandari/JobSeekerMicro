using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementService.Application.Features.Feature.Queries
{
    public record GetAllFeaturesQuery : IRequest<List<AdvertisementService.Domain.Entities.Feature>>;
}
