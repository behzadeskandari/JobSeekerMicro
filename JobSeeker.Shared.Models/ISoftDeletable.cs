using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Models
{
    public interface ISoftDeletable
    {
        bool? IsActive { get; set; }
        DateTime? DateModified { get; set; }
    }
}
