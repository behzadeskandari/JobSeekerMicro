using Microsoft.EntityFrameworkCore;
using ProfileService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Persistance.DbContexts
{
    public class ProfileServiceDbContext : DbContext
    {
        public ProfileServiceDbContext(DbContextOptions<ProfileServiceDbContext> options)
: base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateJobPreferences> CandidateJobPreferences { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<OutboxMessage> OutboxMessage { get; set; }
        public DbSet<Resume> Resume { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        
        

    }
}
