using AppJob.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJob.Core.Core
{
    public interface ILinkService
    {
        Task<string> GenerateUniqueLinkAsync(string purpose, DateTime? expirationDate = null, string? associatedData = null);
        Task<bool> ValidateLinkAsync(string token);
        Task<GeneratedLink?> GetLinkDetailsAsync(string token);
    }
}
