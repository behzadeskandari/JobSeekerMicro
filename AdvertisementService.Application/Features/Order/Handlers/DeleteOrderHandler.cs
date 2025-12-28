using System.Threading.Tasks;
using AdvertisementService.Application.Features.Order.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Order.Handlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public DeleteOrderHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.OrdersRepository.GetByIdAsync(request.Id);
            if (order == null)
            {
                return Result.Fail("Order not found");
            }

            await _repository.OrdersRepository.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

