using System;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Payment.Command
{
    public class DeletePaymentCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}

