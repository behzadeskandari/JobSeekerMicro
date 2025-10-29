using AppJob.Core.Domain.Entities;
using AppJob.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppJob.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace AppJob.Core.Repository
{
    public class LinkRepository : ILinkRepository
    {
        private readonly MyServiceDbContext _dbContext; // Assuming you can reuse your existing DbContext

        public LinkRepository(MyServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GeneratedLink> AddAsync(GeneratedLink link)
        {
            _dbContext.GeneratedLinks.Add(link);
            await _dbContext.SaveChangesAsync();
            return link;
        }

        public async Task<GeneratedLink?> GetByTokenAsync(string token)
        {
            return await _dbContext.GeneratedLinks.FirstOrDefaultAsync(l => l.Token == token);
        }

        public async Task<bool> IsValidAsync(string token)
        {
            var link = await GetByTokenAsync(token);
            return link != null && (!link.ExpirationDate.HasValue || link.ExpirationDate > DateTime.UtcNow);
        }
    }
}
