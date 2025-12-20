using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobSeeker.Shared.Contracts.Candidate;
using JobSeeker.Shared.Kernel.Utility;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ProfileService.Application.Features.Candidate.Command.CreateCandidateCommand
{
    public class CreateCandidateCommand : IRequest<Result>
    {
        public CandidateCreateDto CandidateDto { get; set; }
        [MaxFilesSize(5)]
        [AllowedExtension(new string[] { ".pdf", ".docx", ".doc" })]
        public IFormFile? PdfFile { get; set; }
    }
}
