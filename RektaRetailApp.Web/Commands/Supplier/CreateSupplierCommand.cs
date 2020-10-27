using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel.Supplier;
using RektaRetailApp.Web.DomainEvents.Supplier;

namespace RektaRetailApp.Web.Commands.Supplier
{
    public class CreateSupplierCommand : IRequest<SupplierApiModel>
    {
        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Description { get; set; }
    }


    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, SupplierApiModel>
    {
        private readonly ISupplierRepository _repo;
        private readonly IMediator _mediator;

        public CreateSupplierCommandHandler(ISupplierRepository repo, IMediator mediator)
        {
            _repo = repo;
            _mediator = mediator;
        }
        public async Task<SupplierApiModel> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            await _repo.CreateSupplierAsync(request).ConfigureAwait(false);
            await _repo.SaveAsync().ConfigureAwait(false);
            var result = await _repo.GetSupplierBy().ConfigureAwait(false);

            await _mediator.Publish(new SupplierCreatedEvent(result), cancellationToken).ConfigureAwait(false);

            return result;
        }
    }
}
