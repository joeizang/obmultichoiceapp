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
                .NotNull()
                .MaximumLength(50)
                .MinimumLength(2);
            RuleFor(s => s.PhoneNumber)
                .NotEmpty().WithMessage("You must provide a valid phone number!")
                .NotNull()
                .MaximumLength(50)
                .MinimumLength(2);
            RuleFor(s => s.Description)
                .MaximumLength(500)
                .WithMessage("The description given is too long!");
        }
    }
}
