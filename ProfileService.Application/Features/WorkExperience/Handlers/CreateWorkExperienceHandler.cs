using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Kernel.Abstractions;
using MediatR;
using ProfileService.Application.Features.WorkExperience.Command;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.WorkExperience.Handlers
{
    public class CreateWorkExperienceHandler : IRequestHandler<CreateWorkExperienceCommand, Guid>
    {
        private readonly IProfileServiceUnitOfWork _context;

        public CreateWorkExperienceHandler(IProfileServiceUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateWorkExperienceCommand request, CancellationToken cancellationToken)
        {
            var workExperience = new ProfileService.Domain.Entities.WorkExperience
            {
                ResumeId = request.ResumeId,
                JobTitle = request.JobTitle,
                CompanyName = request.CompanyName,
                IsCurrentJob = request.IsCurrentJob,
                Description = request.Description,
                DateCreated = DateTime.Now,
                IsActive = true
            };

            await _context.WorkExperienceRepository.AddAsync(workExperience);
            await _context.CommitAsync(cancellationToken);
            return workExperience.Id;
        }
    }
}
