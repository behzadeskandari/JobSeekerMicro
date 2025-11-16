using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

namespace JobService.Persistence.Repository
{
    public class CompanyJobPreferencesRepository : GenericWriteRepository<CompanyJobPreferences>, ICompanyJobPreferencesRepository
    {
        public CompanyJobPreferencesRepository(JobDbContext context) : base(context)
        {
        }
    }
}
