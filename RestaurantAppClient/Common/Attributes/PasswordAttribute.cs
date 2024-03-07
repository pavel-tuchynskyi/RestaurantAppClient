using System.ComponentModel.DataAnnotations;

namespace RestaurantAppClient.Common.Attributes
{
    public class PasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is not string password)
            {
                return new ValidationResult($"{validationContext.DisplayName} must be the type of string.");
            }

            if(!password.Any(c => char.IsDigit(c) || char.IsUpper(c)))
            {
                return new ValidationResult($"{validationContext.DisplayName} must contain at least one digit and one upper case letter.");
            }          

            return ValidationResult.Success;
        }
    }
}
