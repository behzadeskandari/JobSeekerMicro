using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace JobSeeker.Shared.Kernel.Utility
{
    public class AllowedExtensionAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions;
        public AllowedExtensionAttribute(string[] extensions) 
        {

            _allowedExtensions = extensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var files = value as IFormFile;
            if (files != null) 
            {
                var filename = Path.GetExtension(files.FileName);
                if (_allowedExtensions.Contains(filename.ToLower()))
                {
                    return new ValidationResult($"The file extension {filename} is not allowed. Allowed extensions are: {string.Join(", ", _allowedExtensions)}");
                }    
            }
            return base.IsValid(value, validationContext);
        }
    }
}
