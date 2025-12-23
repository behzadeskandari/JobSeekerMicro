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
    public class DeleteWorkExperienceHandler : IRequestHandler<DeleteWorkExperienceCommand, Result<string>>
    {
        private readonly IProfileServiceUnitOfWork _context;

        public DeleteWorkExperienceHandler(IProfileServiceUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(DeleteWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            var workExperience = await _context.WorkExperienceRepository.GetByIdAsync(request.Id);
            if (workExperience == null)
            {
                return Result.Fail("تجربه کاری پیدا نشد");
            }

            await _context.WorkExperienceRepository.DeleteAsync(workExperience);
            await _context.CommitAsync(cancellationToken);
            return Result.Ok("تجربه کاری با موفقیت پاک شد");
        }
    }
}
