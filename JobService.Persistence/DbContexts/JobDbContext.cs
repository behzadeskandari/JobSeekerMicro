using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AdvertisementService.Persistence.DbContexts;
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

    }
}
