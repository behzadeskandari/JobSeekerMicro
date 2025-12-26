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
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, Result>
    {
        private readonly IJobUnitOfWork _unitOfWork;

        public DeleteCityCommandHandler(IJobUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var city = await _unitOfWork.City.GetByIdAsync(request.Id);
            if (city == null)
            {
                Result.Fail("مشکل در پاک کردن دیتا");
            }
            await _unitOfWork.City.GetByIdAsync(city.Id);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
