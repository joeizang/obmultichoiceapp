using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using ObmultichoiceRetailer.Domain.DomainModels;

namespace ObmultichoiceRetailer.Web.Validations
{
    public class CreateInventoryValidation : AbstractValidator<Inventory>
    {
        public CreateInventoryValidation()
        {
            RuleFor(i => i.BatchNumber)
                .NotEmpty().WithMessage("Batch number cannot be blank" )
                .MaximumLength(50).WithMessage("Batch number has too many characters, maximum is 50!");
            RuleFor(i => i.CategoryId)
                .NotEmpty().NotEqual(0);
            RuleFor(i => i.Description)
                .MaximumLength(450);
            RuleFor(i => i.Name)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(i => i.SupplyDate)
                .NotEmpty();
        }
    }
}
