using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.Customer.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Customer.Handlers
{
    public class GetCustomersHandler : IRequestHandler<GetCustomersQuery, IEnumerable<AdvertisementService.Domain.Entities.Customer>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetCustomersHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AdvertisementService.Domain.Entities.Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _repository.CustomersRepository.GetAllAsync(cancellationToken);
        }
    }
}

