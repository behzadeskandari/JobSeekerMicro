using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using ProfileService.Application.Features.Resume.Queries;
using ProfileService.Application.Interfaces;
using ProfileService.Infrastructure.Services;

namespace ProfileService.Application.Features.Resume.Handlers
{
    public class GetResumePdfQueryHandler : IRequestHandler<GetResumePdfQuery, Result<byte[]>>
    {

        private readonly IProfileServiceUnitOfWork _unitOfWork;
        private readonly IPdfService _pdfService;
        public GetResumePdfQueryHandler(IProfileServiceUnitOfWork unitOfWork, IPdfService pdfService)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<byte[]>> Handle(GetResumePdfQuery request, CancellationToken cancellationToken)
        {
            var resume = await _unitOfWork.ResumeRepository.GetByIdAsync(request.Id);
            if (resume == null)
            {
               return Result.Fail("رزومه مورد نظر پیدا نشد");
            }
            var pdfBytes = await _pdfService.GenerateResumePdf(resume);
            return new Result<byte[]>().WithSuccess(" Created SuccessFully").WithValue(pdfBytes);
        }
    }
}
