using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.Province.Command
{
    public class CreateProvinceCommand : IRequest<Result<int>>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Label { get; set; } = string.Empty;

        [Required]
        public string Value { get; set; } = string.Empty;
    }
}

