using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Kernel.Domain;
using JobSeeker.Shared.Models;

namespace ProfileService.Domain.Entities
{
  public class Candidate : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string CoverLetter { get; set; } = string.Empty;
        public string ResumeUrl { get; set; } = string.Empty;
        public DateTime? LastAppliedDate { get; set; }

        // Relations
        public Guid? ResumeId { get; set; }
        public Resume? Resume { get; set; }

        public Guid? JobId { get; set; }
        //public Job? Job { get; set; } 
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;
        public bool? IsActive { get; set; }

        //[ForeignKey("User")]
        public string UserId { get; set; }
        //public User User { get; set; }
        
        public ICollection<Skill> Skill { get; set; } = new  List<Skill>();


        // Foreign keys for test results (as discussed)
        public Guid? PsychologyTestResultsId { get; set; }
        //public PsychologyTestResult PsychologyTestResult { get; set; }

        //[ForeignKey("PersonalityTestResult")]
        public Guid? PersonalityTestResultsId { get; set; }
        //public PersonalityTestResult PersonalityTestResult { get; set; }

        //[ForeignKey("CandidateJobPreferences")]
        public Guid CandidateJobPreferencesId { get; set; }
        public CandidateJobPreferences CandidateJobPreferences { get; set; }
        public string? MBTIType { get; set; }
        public int YearsOfExperience { get; set; }
        public Guid EducationLevelId { get; set; }
        //[ForeignKey("City")]
        public int CityId { get; set; }
        //public City City { get; set; }

        //   public virtual ICollection<JobApplication> JobApplications { get; set; }


        private readonly List<DomainEvent> _domainEvents = new();

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
