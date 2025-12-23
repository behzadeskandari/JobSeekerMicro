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
    public class GetAllWorkExperiencesHandler : IRequestHandler<GetAllWorkExperiencesQuery, List<ProfileService.Domain.Entities.WorkExperience>>
    {
        private readonly IProfileServiceUnitOfWork _context;

        public GetAllWorkExperiencesHandler(IProfileServiceUnitOfWork context)
        {
            _context = context;
        }

        public async Task<List<ProfileService.Domain.Entities.WorkExperience>> Handle(GetAllWorkExperiencesQuery request, CancellationToken cancellationToken)
        {
            var record = await _context.WorkExperienceRepository.GetAllAsync();
            //.Include(we => we.Resume) // Include the related Resume


            foreach (var item in record)
            {
                var resume = await _context.ResumeRepository.GetByIdAsync(item.ResumeId);

                if (resume != null)
                {
                    item.Resume = resume;
                }
                else
                {
                    item.Resume = new Domain.Entities.Resume();
                }

            }
            return record.ToList();
        }
    }
}
