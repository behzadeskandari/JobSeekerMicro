using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Application.Features.Education.Queries;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.Education.Handlers
{
    public class GetEducationByIdHandler : IRequestHandler<GetEducationByIdQuery, ProfileService.Domain.Entities.Education>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;

        public GetEducationByIdHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Domain.Entities.Education> Handle(GetEducationByIdQuery request, CancellationToken cancellationToken)
        {
            var record = await _unitOfWork.EducationRepository.GetByIdAsync(request.Id); // Filter for active educations

            var resume = await _unitOfWork.ResumeRepository.GetByIdAsync(record.ResumeId);
            if (resume != null)
            {
                record.Resume = resume;
            }
            else;
            {
                record.Resume = new Domain.Entities.Resume();
            }
            return record;
        }
    }
}
