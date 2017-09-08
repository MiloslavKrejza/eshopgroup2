using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop2.Models.Validations
{
    public class IsTrueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(!(value is bool))
                return new ValidationResult(ErrorMessage);

            bool valAsBool = (bool)value;

            return valAsBool ? ValidationResult.Success : new ValidationResult(ErrorMessage);

        }
    }
}
