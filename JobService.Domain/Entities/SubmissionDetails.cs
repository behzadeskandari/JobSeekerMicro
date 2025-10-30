using JobSeeker.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService.Domain.Entities
{
    public class SubmissionDetails : IBaseEntity<Guid>
    {
        [Key]
        [ForeignKey("JobApplication")]
        public Guid ApplicationId { get; set; }
        public virtual JobApplication JobApplication { get; set; }

        public string Source { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public Guid Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }
}
