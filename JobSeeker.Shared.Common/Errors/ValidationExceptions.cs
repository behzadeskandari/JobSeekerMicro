using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Common.Errors
{
    public sealed class ValidationExceptions : BadRequestException, IError
    {
        public ValidationExceptions(Dictionary<string, string[]> errors)
            : base("Validation errors occurred") =>
            Errors = errors;

        public Dictionary<string, string[]> Errors { get; }

        public List<IError> Reasons { get; set; }

        public Dictionary<string, object> Metadata { get; set; }
    }
}
