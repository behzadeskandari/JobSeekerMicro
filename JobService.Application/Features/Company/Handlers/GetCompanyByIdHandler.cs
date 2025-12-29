using System.Threading.Tasks;
using JobService.Application.Features.Company.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.Company.Handlers
{
    public class GetCompanyByIdHandler : IRequestHandler<GetCompanyByIdQuery, JobService.Domain.Entities.Company?>
    {
        private readonly IJobUnitOfWork _repository;

        public GetCompanyByIdHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<JobService.Domain.Entities.Company?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Company.GetByIdAsync(request.Id);
        }
    }
}

