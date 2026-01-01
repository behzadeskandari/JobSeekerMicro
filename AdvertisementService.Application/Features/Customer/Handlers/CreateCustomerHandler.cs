using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.Customer.Command;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Customer.Handlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Result<string>>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public CreateCustomerHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new AdvertisementService.Domain.Entities.Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserId = request.UserId,
                OrdersId = request.OrdersId,
                CustomerType = request.CustomerType,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            await _repository.CustomersRepository.AddAsync(customer);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(customer.Id.ToString());
        }
    }
}

