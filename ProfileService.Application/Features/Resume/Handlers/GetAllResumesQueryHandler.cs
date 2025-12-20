using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileService.Application.Features.Resume.Queries;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.Resume.Handlers
{
    public class GetAllResumesHandler : IRequestHandler<GetAllResumesQuery, List<ProfileService.Domain.Entities.Resume>>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;

        public GetAllResumesHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Domain.Entities.Resume>> Handle(GetAllResumesQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ResumeRepository.GetQueryable()
                .Include(r => r.WorkExperiences)
                .Include(r => r.Educations)
                .Include(r => r.Skills)
                .Include(r => r.Languages)
                .ToListAsync(cancellationToken);
        }
    }
}
