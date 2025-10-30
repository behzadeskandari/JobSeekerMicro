using JobSeeker.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentService.Domain.Entities
{
    public class MBTIQuestions : IBaseEntity<Guid>
    {

        public Guid Id { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;

        public string QuestionText { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        public bool? IsActive { get; set; }

        [ForeignKey("MBTIResult")]
        public Guid? MBTIResultId { get; set; }
        public MBTIResult MBTIResult { get; set; }
    }
}
