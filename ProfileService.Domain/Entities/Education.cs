using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Kernel.Domain;
using JobSeeker.Shared.Models;

namespace ProfileService.Domain.Entities
{
    public class Education : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        [Required]
        [ForeignKey("Resume")]
        public Guid ResumeId { get; set; }
        public Resume Resume { get; set; }

        [Required]
        public string Degree { get; set; }
        [Required]
        public string Institution { get; set; }
        [Required]
        public string Field { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        //[NotMapped]


        private readonly List<DomainEvent> _domainEvents = new();

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }

}
