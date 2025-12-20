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
    public class GetLanguageByIdHandler : IRequestHandler<GetLanguageByIdQuery, ProfileService.Domain.Entities.Language>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;

        public GetLanguageByIdHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Domain.Entities.Language> Handle(GetLanguageByIdQuery request, CancellationToken cancellationToken)
        {
            var record = await _unitOfWork.LanguageRepository.GetQueryable()
            .FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);

            if (record != null)
            {
                var resume = await _unitOfWork.ResumeRepository.GetByIdAsync(record.ResumeId);
                if (resume != null)
                {
                    record.Resume = resume;
                }
                else
                {
                    record.Resume = new Domain.Entities.Resume();
                }
            }

            return record;
        }
    }
}
