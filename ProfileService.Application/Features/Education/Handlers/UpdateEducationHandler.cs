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
    public class UpdateEducationHandler : IRequestHandler<UpdateEducationCommand, Result<string>>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;

        public UpdateEducationHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {
            var education = await _unitOfWork.EducationRepository.GetByIdAsync(request.Id);
            if (education == null)
            {
               return Result.Fail("برنامه تحصیلی پیدا نشد");
            }

            education.ResumeId = request.ResumeId;
            education.Degree = request.Degree;
            education.Institution = request.Institution;
            education.Field = request.Field;
            education.StartDate = request.StartDate;
            education.EndDate = request.EndDate;
            education.Description = request.Description;
            education.DateModified = DateTime.Now;
            education.IsActive = request.IsActive;

            await _unitOfWork.EducationRepository.UpdateAsync(education);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Result.Ok("مدارک تحصیلی با موفقیت ویرایش شد");
        }
    }
}
