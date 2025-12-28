using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.Customer.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Customer.Handlers
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public UpdateCustomerHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.CustomersRepository.GetByIdAsync(request.Id);
            if (customer == null)
            {
                return Result.Fail("Customer not found");
            }

            if (!string.IsNullOrEmpty(request.FirstName))
                customer.FirstName = request.FirstName;
            if (!string.IsNullOrEmpty(request.LastName))
                customer.LastName = request.LastName;
            if (!string.IsNullOrEmpty(request.CustomerType))
                customer.CustomerType = request.CustomerType;
            
            customer.UpdatedOn = DateTime.UtcNow;
            customer.DateModified = DateTime.UtcNow;

            await _repository.CustomersRepository.UpdateAsync(customer);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

