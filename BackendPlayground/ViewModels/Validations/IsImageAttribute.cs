using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace BackendPlayground.ViewModels
{
    internal class IsImageAttribute : ValidationAttribute
    {
       
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            FileUp file = (FileUp)context.ObjectInstance;
            if (!file.File.ContentType.Contains("image"))
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}