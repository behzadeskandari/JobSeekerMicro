using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Kernel.Domain;

namespace JobSeeker.Shared.Models
{
    public class TermsOfService : IBaseEntity<int>
    {
        public int Id { get; set; }
        [Required]
        public string Version { get; set; }
        [Required]
        public string LastUpdated { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        public List<TermsSection> Sections { get; set; } = new List<TermsSection>();



        private readonly List<DomainEvent> _domainEvents = new();

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

    }
}
