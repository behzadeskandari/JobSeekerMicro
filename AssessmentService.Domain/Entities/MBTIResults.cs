using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Models;

namespace AssessmentService.Domain.Entities
{
    public class MBTIResult : IBaseEntity<Guid>
    {

        public Guid Id { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Type { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Result { get; set; } = string.Empty;// New field for calculation result 
        public bool? IsActive { get; set; }

        //[ForeignKey("User")]
        public string? UserId { get; set; }
        //public User? Users { get; set; }
        public ICollection<MBTIQuestions> MBTIQuestions { get; set; } = new List<MBTIQuestions>(); // If linking to questions

    }
}
