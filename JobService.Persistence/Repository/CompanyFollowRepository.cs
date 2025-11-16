using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

namespace JobService.Persistence.Repository
{
    public class CompanyFollowRepository : GenericWriteRepository<CompanyFollow>, ICompanyFollowRepository
    {
        public CompanyFollowRepository(JobDbContext context) : base(context)
        {
        }
    }
}
