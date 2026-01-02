using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobService.Domain.Common;

namespace JobService.Domain.Entities
{
    public class City : AuditableEntityBaseInt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        [Required]
        public int ProvinceId { get; set; }
        [Required]
        public string Value { get; set; } = string.Empty;

        // Navigation property for Jobs
        public ICollection<Job> Jobs { get; set; }
    }
}
