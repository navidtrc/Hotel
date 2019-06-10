using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipHMSDataAnnotations
{
    public class CompareRequiredAttribute : ValidationAttribute
    {
        private string[] _properties { get; set; }
        public CompareRequiredAttribute(params string[] Properties)
        {
            _properties = Properties;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var val = _properties.Select(validationContext.ObjectType.GetProperty)
                .Select(p => p.GetValue(validationContext.ObjectInstance, null))
                .OfType<string>()
                .FirstOrDefault();
            if (val == "true")
                return (!string.IsNullOrEmpty(value.ToString())) ? ValidationResult.Success
                    : new ValidationResult($"{validationContext.DisplayName} is required");
            else if (val == "false")
                return ValidationResult.Success;
            else
                return new ValidationResult($"Error validation, Bad name for SubmitUser");
        }
    }
}
