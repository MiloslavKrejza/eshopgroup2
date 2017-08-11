using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace TestWeb.ViewModels.Validations
{
    /// <summary>
    /// This class provides additional file validation for uploaded images
    /// </summary>
    internal class IsImageAttribute : ValidationAttribute
    {
        
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            IFormFile file = (IFormFile)value;
            if (file != null && !file.ContentType.Contains("image"))
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}