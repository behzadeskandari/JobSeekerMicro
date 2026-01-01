using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using IdentityService.Domain.Entities;
using JobSeeker.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Persistence.DbContext
{
    public class ApplicationUserDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<OutboxMessage> OutboxMessage { get; set; }
        public DbSet<TermsOfService> TermsOfServices { get; set; }
        public DbSet<TermsSection> TermsSections { get; set; }
        public DbSet<AppPushSubscriptions> PushSubscriptions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<User>()
            .HasIndex(a => a.Email)
            .IsUnique();

            // Configure TermsOfService and TermsSection relationship
            modelBuilder.Entity<TermsSection>()
                .HasOne(ts => ts.TermsOfService)
                .WithMany(tos => tos.Sections)
                .HasForeignKey(ts => ts.TermsOfServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ignore DomainEvents property on entities that implement domain events
            // since it's not a database relationship but domain logic for in-memory event handling
            //modelBuilder.Entity<User>().Ignore(e => e.DomainEvents);

            base.OnModelCreating(modelBuilder);

            
            //modelBuilder.AddInboxStateEntity();
            //modelBuilder.AddOutboxMessageEntity();
            //modelBuilder.AddOutboxStateEntity();
        }
    }
}
