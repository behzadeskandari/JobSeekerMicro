using ProfileService.Application.Interfaces;
using ProfileService.Domain.Entities;
using ProfileService.Persistance.DbContexts;
using ProfileService.Persistance.GenericRepository;
namespace ProfileService.Persistance.Repository
{
    public class LanguageRepository : GenericWriteRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(ProfileServiceDbContext context) : base(context)
        {
        }
    }
}
