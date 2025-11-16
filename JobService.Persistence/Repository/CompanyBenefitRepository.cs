using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

namespace JobService.Persistence.Repository
{
    public class CompanyBenefitRepository : GenericWriteRepository<CompanyBenefit>, ICompanyBenefitRepository
    {
        public CompanyBenefitRepository(JobDbContext context) : base(context)
        {
        }
    }
}
