using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using JobSeeker.Shared.Kernel.Abstractions;
using MediatR;
using ProfileService.Application.Features.Candidate.Command.CreateCandidateCommand;
using ProfileService.Application.Interfaces;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Features.Candidate.Handlers.CreateCandidateCommandHandler
{
    public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, Result>
    {
        private readonly IProfileServiceUnitOfWork _unitOfWork;

        public CreateCandidateCommandHandler(IProfileServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            var maxFileSizeBytes = 5 * 1024 * 1024; // 5 MB
            var pdfMimeType = "application/pdf";

            if (request.PdfFile != null)
            {
                if (request?.PdfFile?.Length > maxFileSizeBytes || request?.PdfFile?.ContentType != pdfMimeType)
                {
                    Result.Fail("فایل معتبر نیست");
                }
                var resumeFileName = Guid.NewGuid().ToString() + ".pdf";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "documents", "pdfs", resumeFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.PdfFile.CopyToAsync(stream);
                }
             
            }
            var candidate = new ProfileService.Domain.Entities.Candidate()
            {
                CoverLetter = request.CandidateDto.CoverLetter,
                Email = request.CandidateDto.Email,
                FirstName = request.CandidateDto.FirstName,
                LastName = request.CandidateDto.LastName,
                Phone = request.CandidateDto.Phone,
            };

            var result = await _unitOfWork.CandidateRepository.AddAsync(candidate);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
