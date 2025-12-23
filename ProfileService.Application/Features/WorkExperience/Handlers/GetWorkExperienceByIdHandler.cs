using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Kernel.Abstractions;
using MediatR;
using ProfileService.Application.Features.WorkExperience.Queries;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.WorkExperience.Handlers
{
    public class GetWorkExperienceByIdHandler : IRequestHandler<GetWorkExperienceByIdQuery, ProfileService.Domain.Entities.WorkExperience>
    {
        private readonly IProfileServiceUnitOfWork _context;

        public GetWorkExperienceByIdHandler(IProfileServiceUnitOfWork context)
        {
            _context = context;
        }

        public async Task<ProfileService.Domain.Entities.WorkExperience> Handle(GetWorkExperienceByIdQuery request, CancellationToken cancellationToken)
        {
            var record = await _context.WorkExperienceRepository.GetByIdAsync(request.Id);
            //.Include(we => we.Resume) // Include the related Resume
            //.FirstOrDefaultAsync(we => we.Id == request.Id, cancellationToken);

            var resume = await _context.ResumeRepository.GetByIdAsync(record.ResumeId);

            if (resume != null)
            {
                record.Resume = resume;
            }
            else
            {
                record.Resume = new Domain.Entities.Resume();
            }

            return record;
        }
    }
}
