using FluentResults;
using MediatR;

namespace JobService.Application.Features.OfferDetails.Command
{
    public class DeleteOfferDetailsCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}

