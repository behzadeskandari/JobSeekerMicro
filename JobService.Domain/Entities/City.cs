using JobSeeker.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService.Domain.Entities
{
    public class City : IBaseEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;

        public string Label { get; set; } = string.Empty;
        [Required]
        public int ProvinceId { get; set; }
        public bool? IsActive { get; set; } = true;
        [Required]
        public string Value { get; set; } = string.Empty;


        // Navigation property for Jobs
        public ICollection<Job> Jobs { get; set; }
    }
}
