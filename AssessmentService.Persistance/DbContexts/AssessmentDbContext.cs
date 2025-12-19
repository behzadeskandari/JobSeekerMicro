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
        public AssessmentDbContext(DbContextOptions<AssessmentDbContext> options): base(options)
        {
            
        }

        public DbSet<AnswerOption> Advertisements { get; set; }
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

    }
}
