using System;
using System.Collections.Generic;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Feature.Command
{
    public class UpdateFeatureCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? IconName { get; set; }
        public string? Language { get; set; }
        public ICollection<int>? JobsIds { get; set; }
    }
}

