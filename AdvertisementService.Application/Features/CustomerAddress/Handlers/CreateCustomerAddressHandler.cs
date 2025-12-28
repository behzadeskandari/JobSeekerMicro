using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.CustomerAddress.Command;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.CustomerAddress.Handlers
{
    public class CreateCustomerAddressHandler : IRequestHandler<CreateCustomerAddressCommand, Result<string>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public CreateCustomerAddressHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.CustomersRepository.GetByIdAsync(request.CustomerId);
            if (customer == null)
            {
                return Result.Fail("Customer not found");
            }

            var customerAddress = new AdvertisementService.Domain.Entities.CustomerAddress
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                City = request.City,
                State = request.State,
                PostalCode = request.PostalCode,
                Country = request.Country,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            await _repository.CustomerAddressRepository.AddAsync(customerAddress);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(customerAddress.Id.ToString());
        }
    }
}

