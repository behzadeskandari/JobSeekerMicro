using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.OfferDetails.Queries
{
    public class GetOfferDetailsByIdQuery : IRequest<JobService.Domain.Entities.OfferDetails?>
    {
        public int Id { get; set; }
    }
}

