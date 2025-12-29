using System.Collections.Generic;
using System.Threading.Tasks;
using JobService.Application.Features.TechnicalOptions.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.TechnicalOptions.Handlers
{
    public class GetTechnicalOptionsHandler : IRequestHandler<GetTechnicalOptionsQuery, IEnumerable<TechnicalOption>>
    {
        private readonly IJobUnitOfWork _repository;

        public GetTechnicalOptionsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TechnicalOption>> Handle(GetTechnicalOptionsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.TechnicalOptions.GetAllAsync(cancellationToken);
        }
    }
}

