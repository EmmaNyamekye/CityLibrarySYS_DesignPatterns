using System.ComponentModel.DataAnnotations;

namespace CityLibrarySYS_DesignPatterns.Models
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date.Date >= DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage ?? "Date cannot be in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }
}