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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ignore DomainEvents property on entities that implement domain events
            // since it's not a database relationship but domain logic for in-memory event handling
            //modelBuilder.Entity<Candidate>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<CandidateJobPreferences>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<Education>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<Language>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<Resume>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<Skill>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<UserSetting>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<WorkExperience>().Ignore(e => e.DomainEvents);
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
        public DbSet<Log> Logs { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
    }
}
