using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.CustomerAddress.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.CustomerAddress.Handlers
{
    public class UpdateCustomerAddressHandler : IRequestHandler<UpdateCustomerAddressCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public UpdateCustomerAddressHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var customerAddress = await _repository.CustomerAddressRepository.GetByIdAsync(request.Id);
            if (customerAddress == null)
            {
                return Result.Fail("CustomerAddress not found");
            }

            if (!string.IsNullOrEmpty(request.AddressLine1))
                customerAddress.AddressLine1 = request.AddressLine1;
            if (request.AddressLine2 != null)
                customerAddress.AddressLine2 = request.AddressLine2;
            if (!string.IsNullOrEmpty(request.City))
                customerAddress.City = request.City;
            if (!string.IsNullOrEmpty(request.State))
                customerAddress.State = request.State;
            if (!string.IsNullOrEmpty(request.PostalCode))
                customerAddress.PostalCode = request.PostalCode;
            if (!string.IsNullOrEmpty(request.Country))
                customerAddress.Country = request.Country;
            
            customerAddress.DateModified = DateTime.UtcNow;

            await _repository.CustomerAddressRepository.UpdateAsync(customerAddress);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

