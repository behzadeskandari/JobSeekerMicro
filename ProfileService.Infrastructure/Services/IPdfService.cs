using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.Domain.Entities;

namespace ProfileService.Infrastructure.Services
{
    public interface IPdfService
    {
        Task<byte[]> GenerateResumePdf(Resume resume);
    }
}
