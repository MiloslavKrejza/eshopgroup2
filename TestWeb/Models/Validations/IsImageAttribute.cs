using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace TestWeb.ViewModels.Validations
{
    internal class IsImageAttribute : ValidationAttribute
    {
        private string fileName;


        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            IFormFile file = (IFormFile)value;
            if (!file.ContentType.Contains("image"))
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}