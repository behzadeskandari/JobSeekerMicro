using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobService.Domain.Common;

namespace JobService.Domain.Entities
{
    public class Province : AuditableEntityBaseInt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public string Label { get; set; } = string.Empty;
        [Required]
        public string Value { get; set; } = string.Empty;
        public ICollection<City> Cities { get; set; }
    }
}
