using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using RektaRetailApp.Domain.DomainModels;

namespace RektaRetailApp.Web.Validations
{
    public class ProductValidation : AbstractValidator<Domain.DomainModels.Product>
    {
        public ProductValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Products cannot have empty names")
                .NotNull().WithMessage("Product names cannot be blank spaces")
                .MaximumLength(50).WithMessage("The Product name is too long!")
                .MinimumLength(2).WithMessage("The Product name is too short!");
            RuleFor(p => p.RetailPrice)
                .GreaterThanOrEqualTo(decimal.MaxValue).WithMessage("The value you entered is too large")
                .LessThanOrEqualTo(0).WithMessage("The number you have entered is too small")
                .ScalePrecision(12, 4, true);
            RuleFor(p => p.SuppliedPrice)
                .GreaterThanOrEqualTo(decimal.MaxValue).WithMessage("The value you entered is too large")
                .LessThanOrEqualTo(0).WithMessage("The number you have entered is too small")
                .ScalePrecision(12, 4, true);
            RuleFor(p => p.UnitPrice)
                .GreaterThanOrEqualTo(decimal.MaxValue).WithMessage("The value you entered is too large")
                .LessThanOrEqualTo(0).WithMessage("The number you have entered is too small")
                .ScalePrecision(12, 4, true);
            RuleFor(p => p.SupplierId)
                .NotEqual(0).WithMessage("Product Identifier cannot be 0");
            RuleFor(p => p.ProductCategories)
                .NotNull().WithMessage("Categories must be initialized");
            RuleFor(p => p.ProductSupplier)
                .NotNull().WithMessage("A Product must have a supplier!");
        }
    }
}
