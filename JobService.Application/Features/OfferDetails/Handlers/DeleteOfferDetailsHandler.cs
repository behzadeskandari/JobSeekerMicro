using System.Threading.Tasks;
using JobService.Application.Features.OfferDetails.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.OfferDetails.Handlers
{
    public class DeleteOfferDetailsHandler : IRequestHandler<DeleteOfferDetailsCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public DeleteOfferDetailsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteOfferDetailsCommand request, CancellationToken cancellationToken)
        {
            var offer = await _repository.OfferDetails.GetByIdAsync(request.Id);
            if (offer == null)
            {
                return Result.Fail("OfferDetails not found");
            }

            await _repository.OfferDetails.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

