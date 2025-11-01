using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Kernel.Domain
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();
        public override bool Equals(object obj)
            => ReferenceEquals(this, obj)
               || obj is ValueObject vo
                  && GetType() == vo.GetType()
                  && GetEqualityComponents().SequenceEqual(vo.GetEqualityComponents());
        public override int GetHashCode()
            => GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                            HashCode.Combine(current, obj?.GetHashCode() ?? 0));
    }
}
