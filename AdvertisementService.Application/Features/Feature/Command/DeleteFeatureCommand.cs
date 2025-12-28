using System;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Feature.Command
{
    public class DeleteFeatureCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}

