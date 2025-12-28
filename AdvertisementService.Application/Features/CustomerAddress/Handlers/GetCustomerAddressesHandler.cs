using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.CustomerAddress.Queries;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using MediatR;

namespace AdvertisementService.Application.Features.CustomerAddress.Handlers
{
    public class GetCustomerAddressesHandler : IRequestHandler<GetCustomerAddressesQuery, IEnumerable<AdvertisementService.Domain.Entities.CustomerAddress>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public GetCustomerAddressesHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AdvertisementService.Domain.Entities.CustomerAddress>> Handle(GetCustomerAddressesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.CustomerAddressRepository.GetAllAsync(cancellationToken);
        }
    }
}

