using FluentResults;
using JobSeeker.Shared.Kernel.Abstractions;
using JobService.Application.Features.Cities.Command;
using JobService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService.Application.Features.Cities.Handlers
{
    internal class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, Result>
    {
        private readonly IJobUnitOfWork _unitOfWork;

        public UpdateCityCommandHandler(IJobUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var city = await _unitOfWork.City.GetByIdAsync(request.Dto.Id);
            if (city == null)
                return Result.Fail("شهر مورد نظر پیدا نشد");

            var province = await _unitOfWork.City.GetByIdAsync(request.Dto.ProvinceId);
            if (province != null)
            {
                await _unitOfWork.City.UpdateCityAsync(city, request.Dto.ProvinceId);
            }
            else
            {
                city.Label = request.Dto.Label;
                city.ProvinceId = request.Dto.ProvinceId;
                city.IsActive = request.Dto.IsActive.HasValue ? request.Dto.IsActive.Value : false;
                await _unitOfWork.City.UpdateCityAsync(city, city.ProvinceId);
            }
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
