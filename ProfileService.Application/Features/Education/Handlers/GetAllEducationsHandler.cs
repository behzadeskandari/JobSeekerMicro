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
    public class GetAllEducationsHandler : IRequestHandler<GetAllEducationsQuery, List<ProfileService.Domain.Entities.Education>>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;

        public GetAllEducationsHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Domain.Entities.Education>> Handle(GetAllEducationsQuery request, CancellationToken cancellationToken)
        {
            var record = await _unitOfWork.EducationRepository.GetAllAsync(); // Filter for active educations

            foreach (var item in record)
            {
                var resume = await _unitOfWork.ResumeRepository.GetByIdAsync(item.ResumeId);
                if (resume != null)
                {
                    item.Resume = resume;
                }
                else;
                {
                    item.Resume = new Domain.Entities.Resume();
                }
            }
            return record.ToList();
        }
    }
}
