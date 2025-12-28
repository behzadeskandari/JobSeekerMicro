using System.Threading.Tasks;
using AdvertisementService.Application.Features.Customer.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Customer.Handlers
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public DeleteCustomerHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.CustomersRepository.GetByIdAsync(request.Id);
            if (customer == null)
            {
                return Result.Fail("Customer not found");
            }

            await _repository.CustomersRepository.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

