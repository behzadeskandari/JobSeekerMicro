using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Contracts.Enums;
using JobSeeker.Shared.Kernel.Abstractions;
using JobSeeker.Shared.Kernel.Domain;
using JobSeeker.Shared.Models;

namespace JobService.Domain.Entities
{
    public class Company : IBaseEntity<Guid> ,IAggregateRoot
    {
        public string Name { get; set; } = string.Empty;
        public CompanySize Size { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public string Industry { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public DateTime FoundedDate { get; set; }
        // Relations

        public Guid Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        public bool IsVerified { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        //[ForeignKey("User")]
        public string UserId { get; set; }
        //public User User { get; set; }
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
        public ICollection<Guid> AdvertisementsIds { get; set; }
        public ICollection<CompanyBenefit> Benefits { get; set; } = new List<CompanyBenefit>();

        public int? JobCategoryId { get; set; }
        public int? CityId { get; set; }
        public int? ProvinceId { get; set; }
        public decimal Rating { get; set; }
        public string LogoUrl { get; set; } = string.Empty;

        private readonly List<DomainEvent> _domainEvents = new();

        protected void Raise(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
