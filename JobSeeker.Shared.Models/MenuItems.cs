using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Models
{
    public class MenuItem : IBaseEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int? ParentId { get; set; }
        public MenuItem Parent { get; set; }
        public List<MenuItem> Children { get; set; } = new List<MenuItem>();
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }
}
