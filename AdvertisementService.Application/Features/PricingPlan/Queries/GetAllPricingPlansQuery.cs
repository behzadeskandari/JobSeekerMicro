using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementService.Application.Features.PricingPlan.Queries
{
    public record GetAllPricingPlansQuery : IRequest<List<AdvertisementService.Domain.Entities.PricingPlan>>;

}
