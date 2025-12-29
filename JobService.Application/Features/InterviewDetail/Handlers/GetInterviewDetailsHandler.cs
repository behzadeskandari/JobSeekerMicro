using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobService.Application.Features.InterviewDetail.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.InterviewDetail.Handlers
{
    public class GetInterviewDetailsHandler : IRequestHandler<GetInterviewDetailsQuery, IEnumerable<JobService.Domain.Entities.InterviewDetail>>
    {
        private readonly IJobUnitOfWork _repository;

        public GetInterviewDetailsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobService.Domain.Entities.InterviewDetail>> Handle(GetInterviewDetailsQuery request, CancellationToken cancellationToken)
        {
            var interviews = await _repository.InterviewDetail.GetAllAsync(cancellationToken);

            if (request.ApplicationId.HasValue)
            {
                return interviews.Where(i => i.ApplicationId == request.ApplicationId.Value);
            }

            return interviews;
        }
    }
}

