using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobService.Application.Features.RejectionDetails.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.RejectionDetails.Handlers
{
    public class GetRejectionDetailsHandler : IRequestHandler<GetRejectionDetailsQuery, IEnumerable<JobService.Domain.Entities.RejectionDetails>>
    {
        private readonly IJobUnitOfWork _repository;

        public GetRejectionDetailsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobService.Domain.Entities.RejectionDetails>> Handle(GetRejectionDetailsQuery request, CancellationToken cancellationToken)
        {
            var rejections = await _repository.RejectionDetails.GetAllAsync(cancellationToken);

            if (request.ApplicationId.HasValue)
            {
                return rejections.Where(r => r.ApplicationId == request.ApplicationId.Value);
            }

            return rejections;
        }
    }
}

