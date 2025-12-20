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
    public class DeleteLanguageHandler : IRequestHandler<DeleteLanguageCommand, Result<string>>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;

        public DeleteLanguageHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = await _unitOfWork.LanguageRepository.GetByIdAsync(request.Id);
            if (language == null)
            {
                return Result.Fail("زبان پیدا نشد");
            }

            await _unitOfWork.LanguageRepository.DeleteAsync(language);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Result.Ok("زبان مورد نظر با موفقیت حذف شد");
        }
    }
}
