using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobService.Application.Features.SubmissionDetails.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.SubmissionDetails.Handlers
{
    public class GetSubmissionDetailsHandler : IRequestHandler<GetSubmissionDetailsQuery, IEnumerable<JobService.Domain.Entities.SubmissionDetails>>
    {
        private readonly IJobUnitOfWork _repository;

        public GetSubmissionDetailsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobService.Domain.Entities.SubmissionDetails>> Handle(GetSubmissionDetailsQuery request, CancellationToken cancellationToken)
        {
            var submissions = await _repository.SubmissionDetails.GetAllAsync(cancellationToken);

            if (request.ApplicationId.HasValue)
            {
                return submissions.Where(s => s.ApplicationId == request.ApplicationId.Value);
            }

            return submissions;
        }
    }
}

