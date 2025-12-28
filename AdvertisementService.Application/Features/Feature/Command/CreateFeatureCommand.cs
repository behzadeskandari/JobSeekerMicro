using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Feature.Command
{
    public class CreateFeatureCommand : IRequest<Result<string>>
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string IconName { get; set; } = string.Empty;

        [Required]
        public string Language { get; set; } = string.Empty;

        public ICollection<int> JobsIds { get; set; } = new List<int>();
    }
}

