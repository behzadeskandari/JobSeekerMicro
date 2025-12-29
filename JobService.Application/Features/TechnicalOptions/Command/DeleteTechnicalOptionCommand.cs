using FluentResults;
using MediatR;

namespace JobService.Application.Features.TechnicalOptions.Command
{
    public class DeleteTechnicalOptionCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}

