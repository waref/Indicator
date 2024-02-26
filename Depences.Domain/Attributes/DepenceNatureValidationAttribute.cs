using System.ComponentModel.DataAnnotations;
namespace Depences.Domain.Attributes
{
    public class DepenceNatureValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null || string.IsNullOrEmpty(value as string)) { return new ValidationResult("Champ Nature est vide !"); }
            var nature = value as string;

            if (!IsValidNature(nature))
            {
                return new ValidationResult($"Champs Nature est invalide , les valeurs possibles sont: Restaurant, Hotel, et Misc.");
            }

            return ValidationResult.Success;
        }

        private bool IsValidNature(string nature)
        {
            return nature == "Restaurant" || nature == "Hotel" || nature == "Misc";
        }
    }
}
