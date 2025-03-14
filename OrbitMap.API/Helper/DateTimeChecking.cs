using System.ComponentModel.DataAnnotations;
using OrbitMap.API.Utils;
using OrbitMap.Domain.Utils;

namespace OrbitMap.API.Helper;

public class DateTimeChecking : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime dateTime)
        {
            if (dateTime > TimeUtil.GetCurrentSEATime())
            {
                return ValidationResult.Success!;
            }

            return new ValidationResult("The date must be in the future.");
        }

        return new ValidationResult("Invalid date format.");
    }
}