using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Models
{
    public interface IBaseEntity<TKey> : ISoftDeletable
    {
        TKey Id { get; set; }
        DateTime? DateCreated { get; set; }
    }
}
