using System.Threading.Tasks;
using JobService.Application.Features.OfferDetails.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.OfferDetails.Handlers
{
    public class GetOfferDetailsByIdHandler : IRequestHandler<GetOfferDetailsByIdQuery, JobService.Domain.Entities.OfferDetails?>
    {
        private readonly IJobUnitOfWork _repository;

        public GetOfferDetailsByIdHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<JobService.Domain.Entities.OfferDetails?> Handle(GetOfferDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.OfferDetails.GetByIdAsync(request.Id);
        }
    }
}

