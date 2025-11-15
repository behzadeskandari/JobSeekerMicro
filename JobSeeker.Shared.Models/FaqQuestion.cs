using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JobSeeker.Shared.Kernel.Domain;

namespace JobSeeker.Shared.Models
{
    public class FaqQuestion : IBaseEntity<int>
    {
        public int Id { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public string Answer { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("FaqCategory")]
        public int? FaqCategoryId { get; set; }
        [JsonIgnore]
        public FaqCategory? FaqCategory { get; set; }


        private readonly List<DomainEvent> _domainEvents = new();

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
