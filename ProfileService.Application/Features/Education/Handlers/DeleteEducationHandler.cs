using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using ProfileService.Application.Features.Education.Command;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.Education.Handlers
{
    public class DeleteEducationHandler : IRequestHandler<DeleteEducationCommand, Result<string>>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;

        public DeleteEducationHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
        {
            var education = await _unitOfWork.EducationRepository.GetByIdAsync(request.Id);
            if (education == null)
            {
                return Result.Fail("برنامه تحصیلی پیدا نشد");
            }

            await _unitOfWork.EducationRepository.DeleteAsync(education);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Result.Ok("تحصیلات کاندیدا با موفقیت پاک شد");
        }
    }
}
