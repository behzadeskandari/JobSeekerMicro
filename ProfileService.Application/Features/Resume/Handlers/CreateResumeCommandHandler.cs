using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using ProfileService.Application.Features.Resume.Command;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.Resume.Handlers
{
    public class CreateResumeCommandHandler : IRequestHandler<CreateResumeCommand, Result<Domain.Entities.Resume>>
    {

        private readonly IProfileServiceUnitOfWork _unitOfWork;

        public CreateResumeCommandHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Domain.Entities.Resume>> Handle(CreateResumeCommand request, CancellationToken cancellationToken)
        {
            request.Resume.CreatedAt = DateTime.Now;
            request.Resume.UpdatedAt = DateTime.Now;

            var record = await _unitOfWork.ResumeRepository.AddAsync(request.Resume);
            if (record.IsPersisted)
            {

                return new Result<Domain.Entities.Resume>()
                    .WithSuccess("رزومه با موفقیت ایجاد شد")
                    .WithValue(request.Resume);
            }
            else
            {
                return new Result<Domain.Entities.Resume>()
                    .WithError("خطا در ایجاد رزومه کاندیدا")
                    .WithValue(request.Resume);
            }
        }
    }
}
