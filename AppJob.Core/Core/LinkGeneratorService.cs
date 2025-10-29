using AppJob.Core.Domain.Entities;
using AppJob.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJob.Core.Core
{
    public class LinkGeneratorService : ILinkGenerator
    {
        public string GenerateUniqueToken()
        {
            // Simple example using GUID and base64 encoding
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("=", "").Replace("+", "");

            // More sophisticated short link generation libraries exist if needed.
        }

        public GeneratedLink CreateLink(string purpose, DateTime? expirationDate = null, string? associatedData = null)
        {
            return new GeneratedLink
            {
                Token = GenerateUniqueToken(),
                Purpose = purpose,
                ExpirationDate = expirationDate,
                AssociatedData = associatedData
            };
        }
    }
}
