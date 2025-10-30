using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Models;

namespace ProfileService.Domain.Entities
{
    public class WorkExperience : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        [Required]
        [ForeignKey("Resume")]
        public Guid ResumeId { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public bool IsCurrentJob { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }

        public Resume Resume { get; set; }
    }
}
