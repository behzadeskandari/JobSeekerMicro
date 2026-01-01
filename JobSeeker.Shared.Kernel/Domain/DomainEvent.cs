using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobSeeker.Shared.Kernel.Domain
{
    public abstract class DomainEvent : INotification
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
