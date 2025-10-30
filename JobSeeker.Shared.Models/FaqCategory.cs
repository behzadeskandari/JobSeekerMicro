using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Models
{
    public class FaqCategory : IBaseEntity<int>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<FaqQuestion> Questions { get; set; } = new List<FaqQuestion>();
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }
}
