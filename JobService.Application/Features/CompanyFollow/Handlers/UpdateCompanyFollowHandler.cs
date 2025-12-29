using System;
using System.Threading.Tasks;
using JobService.Application.Features.CompanyFollow.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.CompanyFollow.Handlers
{
    public class UpdateCompanyFollowHandler : IRequestHandler<UpdateCompanyFollowCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public UpdateCompanyFollowHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateCompanyFollowCommand request, CancellationToken cancellationToken)
        {
            var follow = await _repository.CompanyFollow.GetByIdAsync(request.Id);
            if (follow == null)
            {
                return Result.Fail("CompanyFollow not found");
            }

            if (request.Rating.HasValue)
                follow.Rating = request.Rating.Value;

            follow.DateModified = DateTime.UtcNow;

            await _repository.CompanyFollow.UpdateAsync(follow);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

