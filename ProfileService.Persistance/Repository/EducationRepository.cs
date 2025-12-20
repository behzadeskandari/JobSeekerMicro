using ProfileService.Application.Interfaces;
using ProfileService.Domain.Entities;
using ProfileService.Persistance.DbContexts;
using ProfileService.Persistance.GenericRepository;
namespace ProfileService.Persistance.Repository
{
    public class EducationRepository : GenericWriteRepository<Education>, IEducationRepository
    {
        public EducationRepository(ProfileServiceDbContext context) : base(context)
        {
        }
    }
}
