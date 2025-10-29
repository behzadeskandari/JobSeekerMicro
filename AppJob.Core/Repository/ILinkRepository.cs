using AppJob.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJob.Core.Interfaces
{
    public interface ILinkRepository
    {
        Task<GeneratedLink> AddAsync(GeneratedLink link);
        Task<GeneratedLink?> GetByTokenAsync(string token);
        Task<bool> IsValidAsync(string token);
    }
}
