using FluentResults;
using JobSeeker.Shared.Contracts.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementService.Application.Features.Order.Queries
{
    public class GetUserOrdersQuery : MediatR.IRequest<Result<IEnumerable<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}
