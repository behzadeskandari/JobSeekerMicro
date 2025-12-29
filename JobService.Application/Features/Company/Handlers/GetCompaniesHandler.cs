using System.Collections.Generic;
using System.Threading.Tasks;
using JobService.Application.Features.Company.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.Company.Handlers
{
    public class GetCompaniesHandler : IRequestHandler<GetCompaniesQuery, IEnumerable<JobService.Domain.Entities.Company>>
    {
        private readonly IJobUnitOfWork _repository;

        public GetCompaniesHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobService.Domain.Entities.Company>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Company.GetAllAsync(cancellationToken);
        }
    }
}

