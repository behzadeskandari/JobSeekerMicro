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
    public class Province : IBaseEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int Id { get; set; }
        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;
        [Required]
        public string Label { get; set; } = string.Empty;
        [Required]
        public string Value { get; set; } = string.Empty;
        public ICollection<City> Cities { get; set; }
        public bool? IsActive { get; set; }
    }
}
