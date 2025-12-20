using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Kernel.Abstractions;
using JobSeeker.Shared.Kernel.Domain;
using JobSeeker.Shared.Models;

namespace ProfileService.Domain.Entities
{
    public class Resume : IBaseEntity<Guid> ,IAggregateRoot
    {
        public Guid Id { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        //public User User { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Phone { get; set; }
        [Required] public string Address { get; set; }
        [Required] public string ProfilePictureUrl { get; set; }
        [Required] public string Summary { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public ICollection<Candidate> Candidates { get; set; }
        public ICollection<WorkExperience> WorkExperiences { get; set; }
        public ICollection<Education> Educations { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public ICollection<Language> Languages { get; set; }

        [NotMapped] public bool IsPersisted { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }

        //public Customer? Customer { get; set; }


        private readonly List<DomainEvent> _domainEvents = new();

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
