using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Kernel.Utilities
{
    public static class Guard
    {
        public static void AgainstNull<T>(T value, string parameterName)
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);
        }

        public static void AgainstNullOrEmpty(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{parameterName} cannot be null or empty.", parameterName);
        }

        public static void AgainstNegativeOrZero(decimal value, string parameterName)
        {
            if (value <= 0)
                throw new ArgumentException($"{parameterName} must be greater than zero.", parameterName);
        }
    }
}
