using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobSeeker.Shared.Kernel.Abstractions;
using MediatR;
using ProfileService.Application.Features.WorkExperience.Command;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.WorkExperience.Handlers
{
    public class UpdateWorkExperienceHandler : IRequestHandler<UpdateWorkExperienceCommand, Result<string>>
    {
        private readonly IProfileServiceUnitOfWork _context;

        public UpdateWorkExperienceHandler(IProfileServiceUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(UpdateWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            var workExperience = await _context.WorkExperienceRepository.GetByIdAsync(request.Id);
            if (workExperience == null)
            {
                return Result.Fail("تجربه کاری پیدا نشد");
            }

            workExperience.ResumeId = request.ResumeId;
            workExperience.JobTitle = request.JobTitle;
            workExperience.CompanyName = request.CompanyName;
            workExperience.IsCurrentJob = request.IsCurrentJob;
            workExperience.Description = request.Description;
            workExperience.DateModified = DateTime.Now;
            workExperience.IsActive = request.IsActive;

            await _context.WorkExperienceRepository.UpdateAsync(workExperience);
            await _context.CommitAsync(cancellationToken);
            return Result.Ok("تجربه کاری با موفقیت ویرایش شد");
        }
    }
}
