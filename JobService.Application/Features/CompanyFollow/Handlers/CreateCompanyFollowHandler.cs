using System;
using System.Threading.Tasks;
using JobService.Application.Features.CompanyFollow.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.CompanyFollow.Handlers
{
    public class CreateCompanyFollowHandler : IRequestHandler<CreateCompanyFollowCommand, Result<int>>
    {
        private readonly IJobUnitOfWork _repository;

        public CreateCompanyFollowHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CreateCompanyFollowCommand request, CancellationToken cancellationToken)
        {
            var company = await _repository.Company.GetByIdAsync(request.CompanyId);
            if (company == null)
            {
                return Result.Fail("Company not found");
            }

            var follow = new JobService.Domain.Entities.CompanyFollow
            {
                UserId = request.UserId,
                CompanyId = request.CompanyId,
                Rating = request.Rating,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            follow.RaiseDomainEvent(new CompanyFollowedEvent(
                follow.Id,
                follow.UserId,
                request.CompanyId,
                DateTime.UtcNow));

            await _repository.CompanyFollow.AddAsync(follow);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(follow.Id);
        }
    }
}

