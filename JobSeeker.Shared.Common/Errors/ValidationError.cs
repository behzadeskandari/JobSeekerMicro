using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Common.Errors
{
    public class ValidationError : FluentValidationError
    {
        public ValidationError(string code, string message) : base(code, message) { }
    }

    public class ConflictError : Error
    {
        public ConflictError(string message) : base(message) { }
    }

    public class NotFoundError : Error
    {
        public NotFoundError(string message) : base(message) { }
    }

    public class UnauthorizedError : Error
    {
        public UnauthorizedError(string message) : base(message) { }
    }
}
