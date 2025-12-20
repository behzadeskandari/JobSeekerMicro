using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProfileService.Application.Features.Language.Command;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.Language.Handlers
{
    public class CreateLanguageHandler : IRequestHandler<CreateLanguageCommand, Guid>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;

        public CreateLanguageHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = new ProfileService.Domain.Entities.Language
            {
                ResumeId = request.ResumeId,
                Name = request.Name,
                ProficiencyLevel = request.ProficiencyLevel,
                DateCreated = DateTime.Now,
                IsActive = true
            };

            await _unitOfWork.LanguageRepository.AddAsync(language);
            await _unitOfWork.CommitAsync(cancellationToken);
            return language.Id;
        }
    }
}
