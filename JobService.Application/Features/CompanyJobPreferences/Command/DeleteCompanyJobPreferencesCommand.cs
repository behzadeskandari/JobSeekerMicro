using FluentResults;
using MediatR;

namespace JobService.Application.Features.CompanyJobPreferences.Command
{
    public class DeleteCompanyJobPreferencesCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}

