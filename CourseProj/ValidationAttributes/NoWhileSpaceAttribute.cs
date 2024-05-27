using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CourseProj.ValidationAttributes;

public class NoWhitespaceAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return new ValidationResult("The field cannot contain only whitespace.");
            }
        }

        return ValidationResult.Success;
    }

    
}

