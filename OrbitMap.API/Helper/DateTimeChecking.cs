using System.ComponentModel.DataAnnotations;

namespace OrbitMap.API.Helper;

public class DateTimeChecking : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime dateTime)
        {
            if (dateTime > DateTime.UtcNow)
            {
                return ValidationResult.Success!;
            }

            return new ValidationResult("The date must be in the future.");
        }

        return new ValidationResult("Invalid date format.");
    }
}