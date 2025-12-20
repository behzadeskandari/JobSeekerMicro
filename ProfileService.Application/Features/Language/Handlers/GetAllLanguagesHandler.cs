using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileService.Application.Features.Language.Queries;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.Language.Handlers
{
    public class GetAllLanguagesHandler : IRequestHandler<GetAllLanguagesQuery, List<ProfileService.Domain.Entities.Language>>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;

        public GetAllLanguagesHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Domain.Entities.Language>> Handle(GetAllLanguagesQuery request, CancellationToken cancellationToken)
        {
            var record = await _unitOfWork.LanguageRepository.GetQueryable()
               //.Include(l => l.ResumeId) // Include the related Resume
               .ToListAsync(cancellationToken);

            foreach (var item in record)
            {
                var resume = await _unitOfWork.ResumeRepository.GetByIdAsync(item.ResumeId);
                if (resume != null)
                {
                    item.Resume = resume;
                }
                else
                {
                    item.Resume = new Domain.Entities.Resume();
                }
            }
            return record;
        }
    }
}
