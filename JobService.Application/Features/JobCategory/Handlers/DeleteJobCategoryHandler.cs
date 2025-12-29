using System.Threading.Tasks;
using JobService.Application.Features.JobCategory.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobCategory.Handlers
{
    public class DeleteJobCategoryHandler : IRequestHandler<DeleteJobCategoryCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public DeleteJobCategoryHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteJobCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.JobCategory.GetByIdAsync(request.Id);
            if (category == null)
            {
                return Result.Fail("JobCategory not found");
            }

            await _repository.JobCategory.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

