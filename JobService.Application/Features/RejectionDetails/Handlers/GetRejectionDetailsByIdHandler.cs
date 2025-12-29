using System.Threading.Tasks;
using JobService.Application.Features.RejectionDetails.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.RejectionDetails.Handlers
{
    public class GetRejectionDetailsByIdHandler : IRequestHandler<GetRejectionDetailsByIdQuery, JobService.Domain.Entities.RejectionDetails?>
    {
        private readonly IJobUnitOfWork _repository;

        public GetRejectionDetailsByIdHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<JobService.Domain.Entities.RejectionDetails?> Handle(GetRejectionDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.RejectionDetails.GetByIdAsync(request.Id);
        }
    }
}

