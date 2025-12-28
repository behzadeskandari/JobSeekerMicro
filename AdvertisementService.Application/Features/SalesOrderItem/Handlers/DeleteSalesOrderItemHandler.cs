using System.Threading.Tasks;
using AdvertisementService.Application.Features.SalesOrderItem.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrderItem.Handlers
{
    public class DeleteSalesOrderItemHandler : IRequestHandler<DeleteSalesOrderItemCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public DeleteSalesOrderItemHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteSalesOrderItemCommand request, CancellationToken cancellationToken)
        {
            var salesOrderItem = await _repository.SalesOrderItemRepository.GetByIdAsync(request.Id);
            if (salesOrderItem == null)
            {
                return Result.Fail("SalesOrderItem not found");
            }

            await _repository.SalesOrderItemRepository.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

