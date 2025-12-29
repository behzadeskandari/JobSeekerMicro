using System.Threading.Tasks;
using JobService.Application.Features.CompanyJobPreferences.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.CompanyJobPreferences.Handlers
{
    public class DeleteCompanyJobPreferencesHandler : IRequestHandler<DeleteCompanyJobPreferencesCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public DeleteCompanyJobPreferencesHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteCompanyJobPreferencesCommand request, CancellationToken cancellationToken)
        {
            var preference = await _repository.CompanyJobPreferences.GetByIdAsync(request.Id);
            if (preference == null)
            {
                return Result.Fail("CompanyJobPreferences not found");
            }

            await _repository.CompanyJobPreferences.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

