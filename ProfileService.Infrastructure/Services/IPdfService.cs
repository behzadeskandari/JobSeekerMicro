using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Contracts.Resume;
using ProfileService.Domain.Entities;

namespace ProfileService.Infrastructure.Services
{
    public interface IPdfService
    {
        Task<byte[]> GenerateResumePdf(ResumeDto resume);
    }


    public interface IHtmlRenderer
    {
        Task<string> RenderResumeAsync(ResumeDto resume);
    }
}
