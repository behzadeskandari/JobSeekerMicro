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
   public class CandidateJobPreferences : IBaseEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        //public User User { get; set; }

        public string PreferredJobType { get; set; }
        public string PreferredLocation { get; set; }
        public string PreferredIndustry { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ExpectedSalary { get; set; }

        // Add other preference fields as needed
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        [ForeignKey("City")]

        public int PreferredCityId { get; set; }
        //public City City { get; set; }

        public int? JobCategoryId { get; set; }
        //public JobCategory? JobCategory { get; set; }
        public string JobType { get; set; }
        public decimal? MinSalary { get; set; }



        private readonly List<DomainEvent> _domainEvents = new();

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
