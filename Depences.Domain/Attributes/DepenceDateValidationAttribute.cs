using System.ComponentModel.DataAnnotations;

namespace Depences.Domain.Attributes
{
    public class DepenceDateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null) { return new ValidationResult("Date est vide!"); }

            if ((DateTime)value > DateTime.Now)
            {
                return new ValidationResult("La date de dépence ne peux pas être au future.");
            }
            DateTime dateMaxValidtite = ((DateTime)value).AddMonths(3);
            if(dateMaxValidtite < DateTime.Now){
                return new ValidationResult("La date de dépence ne peux pas dater plus que 3 mois.");
            }

            return ValidationResult.Success;
        }
    }
}
