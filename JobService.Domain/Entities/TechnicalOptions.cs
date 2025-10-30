using JobSeeker.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService.Domain.Entities
{
    public class TechnicalOption : IBaseEntity<int>
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;
        public string Label { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public bool? IsActive { get; set; }

        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
