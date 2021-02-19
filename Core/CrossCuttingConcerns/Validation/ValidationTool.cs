using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool // sürekli instance almamak için static oluşturduk. (genelede de böyle olur)
    {
        // C# da static class'ın metotlarıda static olmalı!! javada metotların static olmasına gerek yok.
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                 throw new ValidationException(result.Errors);
            }
        }
}
}
