using AppJob.Core.Core;
using AppJob.Core.Domain.Entities;
using AppJob.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJob.Core.Repository
{
    public class LinkService : ILinkService
    {
        private readonly ILinkGenerator _linkGenerator;
        private readonly ILinkRepository _linkRepository;

        public LinkService(ILinkGenerator linkGenerator, ILinkRepository linkRepository)
        {
            _linkGenerator = linkGenerator;
            _linkRepository = linkRepository;
        }

        public async Task<string> GenerateUniqueLinkAsync(string purpose, DateTime? expirationDate = null, string? associatedData = null)
        {
            var link = _linkGenerator.CreateLink(purpose, expirationDate, associatedData);
            await _linkRepository.AddAsync(link);
            return link.Token;
        }

        public async Task<bool> ValidateLinkAsync(string token)
        {
            var link = await _linkRepository.GetByTokenAsync(token);
            if (link == null)
            {
                return false;
            }

            return !link.ExpirationDate.HasValue || link.ExpirationDate > DateTime.UtcNow;
        }

        public async Task<GeneratedLink?> GetLinkDetailsAsync(string token)
        {
            return await _linkRepository.GetByTokenAsync(token);
        }
    }
}
