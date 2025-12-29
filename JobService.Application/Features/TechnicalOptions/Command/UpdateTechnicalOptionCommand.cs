using FluentResults;
using MediatR;

namespace JobService.Application.Features.TechnicalOptions.Command
{
    public class UpdateTechnicalOptionCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string? Label { get; set; }
        public string? Value { get; set; }
    }
}

