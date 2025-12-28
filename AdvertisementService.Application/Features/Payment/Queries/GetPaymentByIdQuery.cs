using System;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Payment.Queries
{
    public class GetPaymentByIdQuery : IRequest<AdvertisementService.Domain.Entities.Payment?>
    {
        public Guid Id { get; set; }
    }
}

