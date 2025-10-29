using AppJob.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJob.Core.Interfaces
{
    public interface ILinkGenerator
    {
        string GenerateUniqueToken();
        GeneratedLink CreateLink(string purpose, DateTime? expirationDate = null, string? associatedData = null);
    }
}
