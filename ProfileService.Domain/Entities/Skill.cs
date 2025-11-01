using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Models;
using JobSeeker.Shared.Contracts.Enums;

namespace ProfileService.Domain.Entities
{

    public class Skill : IBaseEntity<int>
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Resume")]
        public Guid ResumeId { get; set; }
        public Resume Resume { get; set; }

        //[ForeignKey("JobPost")]
        public Guid JobPostId { get; set; }
        //public JobPost JobPosts { get; set; }


        [ForeignKey("Candidate")]
        public Guid CandidateId { get; set; }
        public Candidate Candidates { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public ProficiencyLevelEnum ProficiencyLevel { get; set; } // 1-5
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }

}
