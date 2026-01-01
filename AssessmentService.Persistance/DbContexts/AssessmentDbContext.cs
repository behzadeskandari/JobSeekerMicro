using AssessmentService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentService.Persistance.DbContexts
{
    public class AssessmentDbContext : DbContext
    {
        public AssessmentDbContext(DbContextOptions<AssessmentDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PsychologyTestResponse>()
                .HasOne(ptr => ptr.PsychologyTest)
                .WithMany(pt => pt.PsychologyTestResponses)
                .HasForeignKey(ptr => ptr.PsychologyTestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PsychologyTestResponse>()
                .HasOne(ptr => ptr.PsychologyTestQuestion)
                .WithMany(ptq => ptq.PsychologyTestResponses)
                .HasForeignKey(ptr => ptr.PsychologyTestQuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PsychologyTestResponse>()
                 .HasOne(ptr => ptr.TestResult)
                 .WithMany(ptr => ptr.Responses)
                 .HasForeignKey(ptr => ptr.TestResultId)
                 .OnDelete(DeleteBehavior.Restrict);

            // Ignore DomainEvents property on entities that implement domain events
            // since it's not a database relationship but domain logic for in-memory event handling
            //modelBuilder.Entity<AnswerOption>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<MBTIQuestions>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<MBTIResultAnswers>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<MBTIResult>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<PersonalityTestItem>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<PersonalityTestResponse>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<PersonalityTestResults>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<PersonalityTrait>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<PsychologyTestInterpretation>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<PsychologyTestQuestion>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<PsychologyTestResponseAnswer>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<PsychologyTestResponse>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<PsychologyTestResultAnswer>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<PsychologyTestResult>().Ignore(e => e.DomainEvents);
            //modelBuilder.Entity<PsychologyTest>().Ignore(e => e.DomainEvents);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<AnswerOption> AnswerOption { get; set; }
        public DbSet<MBTIQuestions> MBTIQuestions { get; set; }
        public DbSet<MBTIResultAnswers> MBTIResultAnswers { get; set; }
        public DbSet<MBTIResult> MBTIResults { get; set; }
        public DbSet<OutboxMessage> OutboxMessage { get; set; }
        public DbSet<PersonalityTestItem> PersonalityTestItems { get; set; }
        public DbSet<PersonalityTestResponse> PersonalityTestResponses { get; set; }
        public DbSet<PersonalityTestResults> PersonalityTestResults { get; set; }
        public DbSet<PersonalityTrait> PersonalityTraits { get; set; }
        public DbSet<PsychologyTestInterpretation> PsychologyTestInterpretation { get; set; }
        public DbSet<PsychologyTestQuestion> PsychologyTestQuestions { get; set; }
        public DbSet<PsychologyTestResponseAnswer> PsychologyTestResponseAnswers { get; set; }
        public DbSet<PsychologyTestResponse> PsychologyTestResponses { get; set; }
        public DbSet<PsychologyTestResultAnswer> PsychologyTestResultAnswers { get; set; }
        public DbSet<PsychologyTestResult> PsychologyTestResults { get; set; }
        public DbSet<PsychologyTest> PsychologyTests { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
    }
}
