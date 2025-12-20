using ProfileService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.Persistance.GenericRepository;
using ProfileService.Persistance.DbContexts;
using ProfileService.Domain.Entities;
namespace ProfileService.Persistance.Repository
{
    public class UserSettingsRepository : GenericWriteRepository<UserSetting>, IUserSettingsRepository
    {
        public UserSettingsRepository(ProfileServiceDbContext context) : base(context)
        {
        }
    }
}
