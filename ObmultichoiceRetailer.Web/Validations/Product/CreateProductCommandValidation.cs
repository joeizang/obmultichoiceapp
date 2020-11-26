using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using ObmultichoiceRetailer.Web.Commands.Product;

namespace ObmultichoiceRetailer.Web.Validations.Product
{
  public class CreateProductCommandValidation : AbstractValidator<CreateProductCommand>
  {
    public CreateProductCommandValidation()
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
      RuleFor(p => p.CostPrice)
          .GreaterThanOrEqualTo(decimal.MaxValue).WithMessage("The value you entered is too large")
          .LessThanOrEqualTo(0).WithMessage("The number you have entered is too small")
          .ScalePrecision(12, 4, true);
      RuleFor(p => p.Brand)
          .MaximumLength(100).WithMessage("The brand name you entered is too long")
          .NotEmpty()
          .NotNull();
      RuleFor(p => p.Comments)
          .MaximumLength(500).WithMessage("Your comments are too long");
    }
  }
}
