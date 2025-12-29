using System.Collections.Generic;
using JobService.Domain.Common;

namespace JobService.Domain.Entities
{
    public class TechnicalOption : EntityBaseInt
    {
        public string Label { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
