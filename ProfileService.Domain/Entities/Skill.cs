using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Contracts.Enums;
using JobSeeker.Shared.Kernel.Abstractions;
using JobSeeker.Shared.Kernel.Domain;
using JobSeeker.Shared.Models;

namespace ProfileService.Domain.Entities
{

    public class Skill : IBaseEntity<int> , IAggregateRoot
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Resume")]
        public Guid ResumeId { get; set; }
        public Resume Resume { get; set; }

        //[ForeignKey("JobPost")]
        public Guid JobPostId { get; set; }
        //public JobPost JobPosts { get; set; }


        [ForeignKey("Candidate")]
        public Guid CandidateId { get; set; }
        public Candidate Candidates { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public ProficiencyLevelEnum ProficiencyLevel { get; set; } // 1-5
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }


        private readonly List<DomainEvent> _domainEvents = new();

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }

}
