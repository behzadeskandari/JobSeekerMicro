using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace JobService.Persistence.Repository
{
    public class CompanyFollowRepository : GenericWriteRepository<CompanyFollow>, ICompanyFollowRepository
    {
        private readonly JobDbContext dbContext;
        public CompanyFollowRepository(JobDbContext context) : base(context)
        {
            dbContext = context;
        }

        public async Task<IEnumerable<CompanyFollow>> GetByUserIdIdAsync(string? userId, CancellationToken cancellationToken)
        {
            var record =  dbContext.Set<CompanyFollow>().ToList();

            foreach (var item in record)
            {
                if (!string.IsNullOrEmpty(item.UserId))
                {
                    record = record.Where(f => f.UserId == userId).ToList();
                }

                if (item.CompanyId.HasValue)
                {
                    record = record.Where(f => f.CompanyId.Value == item.CompanyId.Value).ToList();
                }

            }
            return record;
        }
    }
}
