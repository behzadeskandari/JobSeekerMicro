using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobSeeker.Shared.Contracts.Resume;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileService.Application.Features.Resume.Queries;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.Resume.Handlers
{
    public class GetResumeByUserIdQueryHandler : IRequestHandler<GetResumeByUserIdQuery, Result<ResumeDto>>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;

        public GetResumeByUserIdQueryHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<ResumeDto>> Handle(GetResumeByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var resume = await _unitOfWork.ResumeRepository
                    .GetQueryable()
                    .Include(r => r.WorkExperiences)
                    .Include(r => r.Educations)
                    .Include(r => r.Skills)
                    .Include(r => r.Languages)
                    .FirstOrDefaultAsync(r => r.UserId == request.UserId, cancellationToken);

                if (resume == null)
                {
                    return Result.Fail<ResumeDto>("رزومه برای کاربر مشخص شده یافت نشد");
                }

                var resumeDto = new ResumeDto
                {
                    UserId = resume.UserId,
                    FullName = resume.FullName,
                    Email = resume.Email,
                    Phone = resume.Phone,
                    Address = resume.Address,
                    ProfilePictureUrl = resume.ProfilePictureUrl,
                    Summary = resume.Summary,
                    // Map other properties as needed
                };

                return Result.Ok(resumeDto);
            }
            catch (Exception ex)
            {
                return Result.Fail<ResumeDto>(new Error("بازیابی رزومه ناموفق بود").CausedBy(ex));
            }
        }
    }
}
