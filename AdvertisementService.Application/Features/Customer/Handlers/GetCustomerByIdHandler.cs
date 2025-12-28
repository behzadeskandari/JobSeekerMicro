using System.Threading.Tasks;
using AdvertisementService.Application.Features.Customer.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.Customer.Handlers
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, AdvertisementService.Domain.Entities.Customer?>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetCustomerByIdHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<AdvertisementService.Domain.Entities.Customer?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.CustomersRepository.GetByIdAsync(request.Id);
        }
    }
}

