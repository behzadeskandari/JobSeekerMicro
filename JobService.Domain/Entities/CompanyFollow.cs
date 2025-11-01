using JobSeeker.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService.Domain.Entities
{
    public class CompanyFollow : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid? CompanyId { get; set; }
        public Company? Company { get; set; }
        //public User User { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateModified { get; set; }
        public int Rating { get; set; }
    }
}
