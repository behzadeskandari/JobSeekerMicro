using System.Threading.Tasks;
using JobService.Application.Features.Province.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.Province.Handlers
{
    public class DeleteProvinceHandler : IRequestHandler<DeleteProvinceCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public DeleteProvinceHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteProvinceCommand request, CancellationToken cancellationToken)
        {
            var province = await _repository.Province.GetByIdAsync(request.Id);
            if (province == null)
            {
                return Result.Fail("Province not found");
            }

            await _repository.Province.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

