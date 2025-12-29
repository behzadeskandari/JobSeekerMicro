using System.Threading.Tasks;
using JobService.Application.Features.InterviewDetail.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.InterviewDetail.Handlers
{
    public class GetInterviewDetailByIdHandler : IRequestHandler<GetInterviewDetailByIdQuery, JobService.Domain.Entities.InterviewDetail?>
    {
        private readonly IJobUnitOfWork _repository;

        public GetInterviewDetailByIdHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<JobService.Domain.Entities.InterviewDetail?> Handle(GetInterviewDetailByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.InterviewDetail.GetByIdAsync(request.Id);
        }
    }
}

