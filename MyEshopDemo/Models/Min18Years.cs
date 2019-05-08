using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEshopDemo.Models
{
    public class Min18Years : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (RegisterViewModel)validationContext.ObjectInstance;

            var age = DateTime.Now.Year - user.DateOfBirth.Year;

            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Your Age should be over than 18");
        }
    }
}