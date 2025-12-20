using ProfileService.Application.Interfaces;
using ProfileService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.Persistance.GenericRepository;
using ProfileService.Persistance.DbContexts;
namespace ProfileService.Persistance.Repository
{
    public class CandidateJobPreferencesRepository : GenericWriteRepository<CandidateJobPreferences>, ICandidateJobPreferencesRepository
    {
        public CandidateJobPreferencesRepository(ProfileServiceDbContext context) : base(context)
        {
        }
    }
}
