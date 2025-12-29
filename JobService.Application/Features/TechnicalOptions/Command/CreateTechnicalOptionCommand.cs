using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.TechnicalOptions.Command
{
    public class CreateTechnicalOptionCommand : IRequest<Result<int>>
    {
        [Required]
        public string Label { get; set; } = string.Empty;

        [Required]
        public string Value { get; set; } = string.Empty;
    }
}

