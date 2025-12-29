using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobService.Application.Features.OfferDetails.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.OfferDetails.Handlers
{
    public class GetOfferDetailsHandler : IRequestHandler<GetOfferDetailsQuery, IEnumerable<JobService.Domain.Entities.OfferDetails>>
    {
        private readonly IJobUnitOfWork _repository;

        public GetOfferDetailsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobService.Domain.Entities.OfferDetails>> Handle(GetOfferDetailsQuery request, CancellationToken cancellationToken)
        {
            var offers = await _repository.OfferDetails.GetOfferDetails(request,cancellationToken);

            return offers;
        }
    }
}

