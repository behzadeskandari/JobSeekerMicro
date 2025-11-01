using JobSeeker.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementService.Domain.Entities
{
    public class Feature : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string IconName { get; set; }
        [Required]
        public string Language { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }

        // Navigation property for Jobs
        public ICollection<Guid> JobsIds { get; set; }
    }
}
