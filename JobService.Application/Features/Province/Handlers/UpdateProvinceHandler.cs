using System;
using System.Threading.Tasks;
using JobService.Application.Features.Province.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.Province.Handlers
{
    public class UpdateProvinceHandler : IRequestHandler<UpdateProvinceCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public UpdateProvinceHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateProvinceCommand request, CancellationToken cancellationToken)
        {
            var province = await _repository.Province.GetByIdAsync(request.Id);
            if (province == null)
            {
                return Result.Fail("Province not found");
            }

            if (!string.IsNullOrEmpty(request.Label))
                province.Label = request.Label;
            if (!string.IsNullOrEmpty(request.Value))
                province.Value = request.Value;

            province.DateModified = DateTime.UtcNow;

            await _repository.Province.UpdateAsync(province);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

