using System.Threading.Tasks;
using JobService.Application.Features.TechnicalOptions.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.TechnicalOptions.Handlers
{
    public class GetTechnicalOptionByIdHandler : IRequestHandler<GetTechnicalOptionByIdQuery, TechnicalOption?>
    {
        private readonly IJobUnitOfWork _repository;

        public GetTechnicalOptionByIdHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<TechnicalOption?> Handle(GetTechnicalOptionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.TechnicalOptions.GetByIdAsync(request.Id);
        }
    }
}

