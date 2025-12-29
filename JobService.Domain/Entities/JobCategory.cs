using System.Collections.Generic;
using JobService.Domain.Common;

namespace JobService.Domain.Entities
{
    public class JobCategory : AuditableEntityBaseInt
    {
        public string Name { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string Slug { get; set; }
        public string Industry { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
