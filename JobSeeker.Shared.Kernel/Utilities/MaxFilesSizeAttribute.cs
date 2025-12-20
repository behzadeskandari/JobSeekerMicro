using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace JobSeeker.Shared.Kernel.Utility
{
    public class MaxFilesSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFilesSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var files = value as IFormFile;
            if (files != null)
            {
                if (files.Length > (1000000 * _maxFileSize))
                {
                    return new ValidationResult($"The file size exceeds the maximum allowed size of {_maxFileSize / 1024} KB.");
                }
            }
            else
            {
                return new ValidationResult("The value is not a valid file.");
            }
            return base.IsValid(value, validationContext);
        }
    }
}
