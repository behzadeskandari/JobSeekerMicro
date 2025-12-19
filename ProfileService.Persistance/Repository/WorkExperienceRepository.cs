using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProfileService.Persistance.GenericRepository;
using ProfileService.Persistance.DbContexts;
using ProfileService.Domain.Entities;
using ProfileService.Application.Interfaces;
namespace ProfileService.Persistance.Repository
{
    public class WorkExperienceRepository : GenericWriteRepository<WorkExperience>, IWorkExperienceRepository
    {
        public WorkExperienceRepository(ProfileServiceDbContext context) : base(context)
        {
        }
    }
}
