using System.Threading.Tasks;
using JobService.Application.Features.TechnicalOptions.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.TechnicalOptions.Handlers
{
    public class DeleteTechnicalOptionHandler : IRequestHandler<DeleteTechnicalOptionCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public DeleteTechnicalOptionHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteTechnicalOptionCommand request, CancellationToken cancellationToken)
        {
            var technicalOption = await _repository.TechnicalOptions.GetByIdAsync(request.Id);
            if (technicalOption == null)
            {
                return Result.Fail("TechnicalOption not found");
            }

            await _repository.TechnicalOptions.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

