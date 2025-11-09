using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Kernel.Domain;
using JobSeeker.Shared.Kernel.Exceptions;
using JobSeeker.Shared.Kernel.Utilities;

namespace JobService.Domain.ValueObjects
{
    public class SalaryRange : ValueObject
    {
        public decimal Min { get; }
        public decimal Max { get; }

        public SalaryRange(decimal min, decimal max)
        {
            Guard.AgainstNegativeOrZero(min, nameof(min));
            Guard.AgainstNegativeOrZero(max, nameof(max));
            if (min > max) throw new DomainException("Min salary cannot be greater than max salary.");
            Min = min;
            Max = max;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Min;
            yield return Max;
        }
    }
}
