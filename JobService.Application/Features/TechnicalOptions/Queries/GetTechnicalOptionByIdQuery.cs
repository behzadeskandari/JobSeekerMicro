using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.TechnicalOptions.Queries
{
    public class GetTechnicalOptionByIdQuery : IRequest<TechnicalOption?>
    {
        public int Id { get; set; }
    }
}

