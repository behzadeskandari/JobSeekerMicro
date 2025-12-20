using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using ProfileService.Application.Features.Candidate.Command.DeleteCandidateCommand;
using ProfileService.Application.Interfaces;

namespace ProfileService.Application.Features.Candidates.Handlers.DeleteCandidateCommandHandler
{
    public class DeleteCandidateCommandHandler : IRequestHandler<DeleteCandidateCommand, Result<string>>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;
        public DeleteCandidateCommandHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.CandidateRepository.GetQueryable().FirstOrDefault(x => x.Id == request.Id);

            if (result == null)
            {
                return Result.Fail("خطا در حذف کاندیدا");
            }
            var modifiedResult = await _unitOfWork.CandidateRepository.DeleteAsync(result.Id);
               
            await _unitOfWork.CommitAsync();
            
            if (modifiedResult)
            {
                return Result.Ok("کاندیدا با موفقیت حذف شد");
            }

            return Result.Fail("حذف نامزد مشکل دارد");
        }
    }
}
