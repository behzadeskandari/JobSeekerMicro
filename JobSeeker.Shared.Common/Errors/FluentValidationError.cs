using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;

namespace JobSeeker.Shared.Common.Errors
{
    public class FluentValidationError : IError
    {
        public FluentValidationError(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }
        public string Message { get; }
        public Dictionary<string, object> Metadata { get; } = new();
        public List<IError> Reasons { get; } = new();
    }
}
