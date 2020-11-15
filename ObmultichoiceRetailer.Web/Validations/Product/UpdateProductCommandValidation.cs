using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation;
using ObmultichoiceRetailer.Web.Commands.Product;

namespace ObmultichoiceRetailer.Web.Validations.Product
{
    public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Products cannot have empty names")
                .NotNull().WithMessage("Product names cannot be blank spaces")
                .MaximumLength(50).WithMessage("The Product name is too long!")
                .MinimumLength(2).WithMessage("The Product name is too short!");
            RuleFor(p => p.Brand)
                .MaximumLength(50).WithMessage("The Product brand name is too long!")
                .MinimumLength(2).WithMessage("The Product brand name is too short!");
            RuleFor(p => p.Comments)
                .MaximumLength(1500).WithMessage("Brand name is too long!");
            RuleFor(p => p.RetailPrice)
                .GreaterThanOrEqualTo(decimal.MaxValue).WithMessage("The value you entered for Retail is too large")
                .LessThanOrEqualTo(0).WithMessage("The number you have entered for Retail Price is too small")
                .ScalePrecision(12, 4, true);
            RuleFor(p => p.ReorderPoint)
                .GreaterThanOrEqualTo(float.MaxValue)
                .WithMessage("The value you entered for Reorder Point is too large")
                .LessThanOrEqualTo(0).WithMessage("The number you have entered for Reorder Point is too small");
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
            RuleFor(p => p.SupplyDate)
                .GreaterThan(DateTimeOffset.MinValue)
                .WithMessage("Supply Date has to be an actual date!");
        }


        //just experimenting with reflection
        //private void CustomNotEmptyMessage(Type updateProductCommand)
        //{
        //    List<string> messages = new List<string>();
        //    var names = updateProductCommand.ReflectedType?.GetProperties()
        //        .Where(x => x.PropertyType.Name.Equals("String")).Select(x => x.Name).ToList();
        //    foreach (var property in updateProductCommand.ReflectedType?.GetProperties()!)
        //    {
        //        names?.ForEach(x =>
        //        {
        //            if (x.Equals(property.Name))
        //            {
        //                messages.Add($"{x} cannot be empty");
        //            }

        //        });
        //    }
        //}
    }
}
