using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Models
{
    public class TermsSection : IBaseEntity<int>
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("TermsOfService")]
        public int? TermsOfServiceId { get; set; }
        public TermsOfService TermsOfService { get; set; }
    }
}
