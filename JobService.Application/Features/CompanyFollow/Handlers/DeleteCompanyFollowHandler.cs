using System;
using System.Threading.Tasks;
using JobService.Application.Features.CompanyFollow.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.CompanyFollow.Handlers
{
    public class DeleteCompanyFollowHandler : IRequestHandler<DeleteCompanyFollowCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public DeleteCompanyFollowHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteCompanyFollowCommand request, CancellationToken cancellationToken)
        {
            var follow = await _repository.CompanyFollow.GetByIdAsync(request.Id);
            if (follow == null)
            {
                return Result.Fail("CompanyFollow not found");
            }

            follow.RaiseDomainEvent(new CompanyUnfollowedEvent(
                follow.Id,
                follow.UserId,
                follow.CompanyId ?? 0,
                DateTime.UtcNow));

            await _repository.CompanyFollow.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

