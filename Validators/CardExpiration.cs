using System;
using System.ComponentModel.DataAnnotations;
using xsolla_backend_card.Models;

namespace xsolla_backend_card.Validators
{
    /// <summary>
    /// Валидатор для срока действия карты
    /// </summary>
    public class CardExpirationAttribute : ValidationAttribute
    {
        public string GetErrorMessage() =>
            $"Срок действия карты истек";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Date dateExp = (Date)validationContext.ObjectInstance;
            
            if (dateExp.Year >= DateTime.Now.Year && dateExp.Month >= DateTime.Now.Month)
                return ValidationResult.Success;
            
            return new ValidationResult(GetErrorMessage());
        }
    }
}