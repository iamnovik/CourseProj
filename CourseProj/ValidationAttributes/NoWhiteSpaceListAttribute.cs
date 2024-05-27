using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CourseProj.ValidationAttributes;

public class NoWhitespaceListAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is List<string> stringList)
        {
            foreach (var stringVal in stringList)
            {
                if (string.IsNullOrWhiteSpace(stringVal))
                {
                    return new ValidationResult("The field cannot contain only whitespace.");
                }
            }
        }

        return ValidationResult.Success;
    }

    
}

