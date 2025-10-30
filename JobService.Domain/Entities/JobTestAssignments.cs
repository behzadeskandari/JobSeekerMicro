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
    public class JobTestAssignment : IBaseEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Job")]
        public Guid? JobId { get; set; }
        public Job Job { get; set; } 

        //[ForeignKey("PsychologyTest")]
        public int? PsychologyTestId { get; set; }
        //public PsychologyTest PsychologyTest { get; set; }

        //[ForeignKey("PersonalityTestResult")] 
        public Guid? PersonalityTestId { get; set; }
        //public PersonalityTestResult PersonalityTest { get; set; } 

        [Required]
        public bool IsRequired { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public bool? IsActive { get; set; }
    }
}
