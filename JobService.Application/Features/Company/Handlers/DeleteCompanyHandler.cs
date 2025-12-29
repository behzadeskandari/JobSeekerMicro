using System.Threading.Tasks;
using JobService.Application.Features.Company.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.Company.Handlers
{
    public class DeleteCompanyHandler : IRequestHandler<DeleteCompanyCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public DeleteCompanyHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _repository.Company.GetByIdAsync(request.Id);
            if (company == null)
            {
                return Result.Fail("Company not found");
            }

            await _repository.Company.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

