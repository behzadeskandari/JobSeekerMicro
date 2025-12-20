using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobSeeker.Shared.Kernel.Abstractions;
using MediatR;
using ProfileService.Application.Features.Candidate.Command.UpdateCandidateCommand;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.Candidates.Handlers.UpdateCandidateCommandHandler
{
    public class UpdateCandidateCommandHandler : IRequestHandler<UpdateCandidateCommand, Result<string>>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;

        public UpdateCandidateCommandHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            ProfileService.Domain.Entities.Candidate candidate = new ProfileService.Domain.Entities.Candidate();
            candidate.Phone = request.CandidateDto.Phone;
            candidate.FirstName = request.CandidateDto.FirstName;
            candidate.LastName = request.CandidateDto.LastName;
            candidate.Email = request.CandidateDto.Email;
            candidate.CoverLetter = request.CandidateDto.CoverLetter;

            var result = await _unitOfWork.CandidateRepository.UpdateAsync(candidate);

            if (result != null)
            {
                return Result.Ok("اطالاعات کاندیدای مورد نظر ویرایش و بروز رسانی شد");
            }
            return Result.Fail("خطا در اپدیت کاندیدا");
        }


    }
}
