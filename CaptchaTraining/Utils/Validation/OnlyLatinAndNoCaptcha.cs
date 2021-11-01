using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CaptchaTraining.Utils.Validation
{
    public class OnlyLatinAndNoCaptcha: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var name = value as string;
            if (String.IsNullOrWhiteSpace(name))
            {
                return new ValidationResult("Name is empty or white space");
            }
            if (name == "captcha")
            {
                return new ValidationResult(@"Name cannot be 'captcha'");
            }

            if (!isValidLatin(name))
            {
                return new ValidationResult("Name contains Cyrillic");
            }
            
            return ValidationResult.Success;
        }

        private bool isValidLatin(String name)
        {
            return Regex.IsMatch(name, "^[a-zA-Z]*$");
        }
    }
}