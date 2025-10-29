using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Models;

namespace JobService.Domain.Entities
{
    public class SavedJob : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid? JobId { get; set; }
        public Job? Job { get; set; }
        public string UserId { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
