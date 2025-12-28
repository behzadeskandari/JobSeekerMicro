using System.Threading.Tasks;
using AdvertisementService.Application.Features.CustomerAddress.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.CustomerAddress.Handlers
{
    public class GetCustomerAddressByIdHandler : IRequestHandler<GetCustomerAddressByIdQuery, AdvertisementService.Domain.Entities.CustomerAddress?>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetCustomerAddressByIdHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<AdvertisementService.Domain.Entities.CustomerAddress?> Handle(GetCustomerAddressByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.CustomerAddressRepository.GetByIdAsync(request.Id);
        }
    }
}

