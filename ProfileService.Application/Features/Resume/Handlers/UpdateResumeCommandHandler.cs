using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileService.Application.Features.Resume.Command;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.Resume.Handlers
{
    public class UpdateResumeCommandHandler : IRequestHandler<UpdateResumeCommand, Result>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;

        public UpdateResumeCommandHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(UpdateResumeCommand request, CancellationToken cancellationToken)
        {
            if (request.Id != request.Resume.Id)
            {
                return Result.Fail("رزومه مورد نظر پیدا نشد");
            }

            request.Resume.UpdatedAt = DateTime.Now;

            try
            {
                var record = await _unitOfWork.ResumeRepository.UpdateAsync(request.Resume);
                if (record.IsPersisted)
                {
                    return new Result().WithSuccess("اطالاعات ذخیره شد");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _unitOfWork.ResumeRepository.ExistsAsync(x => x.Id == request.Id))
                {
                    return Result.Fail("رزومه مورد نظر پیدا نشد");
                }
                else
                {
                    throw;
                }
            }
            return new Result().WithSuccess("رزومه با موفقیت به‌روزرسانی شد");
        }
    }
}
