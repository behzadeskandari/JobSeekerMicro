using System.Threading.Tasks;
using JobService.Application.Features.SubmissionDetails.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.SubmissionDetails.Handlers
{
    public class GetSubmissionDetailsByIdHandler : IRequestHandler<GetSubmissionDetailsByIdQuery, JobService.Domain.Entities.SubmissionDetails?>
    {
        private readonly IJobUnitOfWork _repository;

        public GetSubmissionDetailsByIdHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<JobService.Domain.Entities.SubmissionDetails?> Handle(GetSubmissionDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.SubmissionDetails.GetByIdAsync(request.Id);
        }
    }
}

