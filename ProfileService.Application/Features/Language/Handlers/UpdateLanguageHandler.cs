using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using ProfileService.Application.Features.Language.Command;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.Language.Handlers
{
    public class UpdateLanguageHandler : IRequestHandler<UpdateLanguageCommand, Result<bool>>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;

        public UpdateLanguageHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = await _unitOfWork.LanguageRepository.GetByIdAsync(request.Id);
            if (language == null)
            {
                return Result.Fail("زبان مورد نظر پیدا نشد");
            }
            language.ResumeId = request.ResumeId;
            language.Name = request.Name;
            language.ProficiencyLevel = request.ProficiencyLevel;
            language.DateModified = DateTime.Now;
            language.IsActive = request.IsActive;

            await _unitOfWork.LanguageRepository.UpdateAsync(language);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Result.Ok(true);
        }
    }
}
