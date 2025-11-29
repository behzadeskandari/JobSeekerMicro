using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

namespace JobService.Persistence.Repository
{
    public class CompanyRepository : GenericWriteRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(JobDbContext context) : base(context)
        {
        }
    }
}
