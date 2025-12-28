using System.Threading.Tasks;
using AdvertisementService.Application.Features.CustomerAddress.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.CustomerAddress.Handlers
{
    public class DeleteCustomerAddressHandler : IRequestHandler<DeleteCustomerAddressCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public DeleteCustomerAddressHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var customerAddress = await _repository.CustomerAddressRepository.GetByIdAsync(request.Id);
            if (customerAddress == null)
            {
                return Result.Fail("CustomerAddress not found");
            }

            await _repository.CustomerAddressRepository.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

