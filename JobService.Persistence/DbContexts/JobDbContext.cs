using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AdvertisementService.Persistence.DbContexts;
using JobSeeker.Shared.Models;
using JobService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobService.Persistence.DbContexts
{
    public class JobDbContext : DbContext
    {
        public JobDbContext(DbContextOptions<JobDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<JobPost>(entity =>
            {
                entity.OwnsOne(j => j.Salary, salary =>
                {
                    salary.Property(p => p.Min)
                          .HasColumnName("SalaryMin")
                          .HasColumnType("decimal(18,2)");

                    salary.Property(p => p.Max)
                          .HasColumnName("SalaryMax")
                          .HasColumnType("decimal(18,2)");
                });
            });

            // Ignore DomainEvents property on entities that inherit from AuditableEntityBaseInt
            // since it's not a database relationship but domain logic for in-memory event handling
            modelBuilder.Entity<SubmissionDetails>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<SavedJob>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<Province>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<RejectionDetails>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<OfferDetails>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<JobTestAssignment>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<JobRequest>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<JobPost>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<JobApplication>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<JobCategory>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<Job>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<InterviewDetail>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<CompanyJobPreferences>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<CompanyFollow>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<CompanyBenefit>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<Company>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<City>().Ignore(e => e.DomainEvents);
        }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<JobRequest> JobRequests { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyBenefit> CompanyBenefits { get; set; }
        public DbSet<CompanyFollow> CompanyFollows { get; set; }
        public DbSet<CompanyJobPreferences> CompanyJobPreferences { get; set; }
        public DbSet<InterviewDetail> InterviewDetail { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<JobApplication> JobApplication { get; set; }
        public DbSet<JobCategory> JobCategory { get; set; }
        public DbSet<JobPost> JobPost { get; set; }
        public DbSet<JobTestAssignment> JobTestAssignments { get; set; }
        public DbSet<OfferDetails> OfferDetails { get; set; }
        public DbSet<OutboxMessage> OutboxMessage { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<RejectionDetails> RejectionDetails { get; set; }
        public DbSet<SavedJob> SavedJob { get; set; }
        public DbSet<SubmissionDetails> SubmissionDetails { get; set; }
        public DbSet<TechnicalOption> TechnicalOptions { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<AppPushSubscriptions> PushSubscriptions { get; set; }

    }
}
