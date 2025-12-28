using System.Threading.Tasks;
using AdvertisementService.Application.Features.SalesOrder.Command;
using AdvertisementService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.SalesOrder.Handlers
{
    public class DeleteSalesOrderHandler : IRequestHandler<DeleteSalesOrderCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public DeleteSalesOrderHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var salesOrder = await _repository.SalesOrderRepository.GetByIdAsync(request.Id);
            if (salesOrder == null)
            {
                return Result.Fail("SalesOrder not found");
            }

            await _repository.SalesOrderRepository.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

