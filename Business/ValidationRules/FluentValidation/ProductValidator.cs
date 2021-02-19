using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        // validasyon (doğrulama) kuralları constructor içine yazılır.
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotNull(); // veya NotEmpty() de olabilir.
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            // kendi eklediğimiz kural
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler 'A' harfi ile başlamalı!");
        }

        private bool StartWithA(string arg) // true = kurala uygun false = kurala uymaz anlamına gelir
        {
            return arg.StartsWith("A");
        }
    }
}
