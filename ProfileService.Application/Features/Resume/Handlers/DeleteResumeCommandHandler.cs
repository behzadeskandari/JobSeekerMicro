using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using ProfileService.Application.Features.Resume.Command;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.Resume.Handlers
{
    public class DeleteResumeCommandHandler : IRequestHandler<DeleteResumeCommand, Result<string>>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;

        public DeleteResumeCommandHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(DeleteResumeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var resume = await _unitOfWork.ResumeRepository.GetByIdAsync(request.Id);
                if (resume == null)
                {
                  return Result.Fail("رزومه مورد نظر یافت نشد");
                }
                await _unitOfWork.ResumeRepository.DeleteAsync(resume);
                await _unitOfWork.CommitAsync();

                return Result.Ok("رزومه مورد نظر با موفقیت پاک شد");
            }
            catch (Exception)
            {
                // Log the exception here if needed
               return Result.Fail("خطا در حذف رزومه مورد نظر");
            }
        }
    }
}
