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
