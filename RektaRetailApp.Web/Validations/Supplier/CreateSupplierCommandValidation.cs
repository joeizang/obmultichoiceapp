using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using RektaRetailApp.Web.Commands.Supplier;

namespace RektaRetailApp.Web.Validations.Supplier
{
    public class CreateSupplierCommandValidation : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierCommandValidation()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("You cannot leave the Name field blank!")
                .NotNull().WithMessage("Name cannot be null")
                .MaximumLength(50).WithMessage("Name is too long")
                .MinimumLength(2).WithMessage("Name is too short");
            RuleFor(s => s.PhoneNumber)
                .NotEmpty().WithMessage("You must provide a valid phone number!")
                .NotNull().WithMessage("Phone number cannot be null")
                .MaximumLength(50)
                .MinimumLength(2);
            RuleFor(s => s.Description)
                .MaximumLength(500)
                .WithMessage("The description given is too long!");
        }
    }
}
