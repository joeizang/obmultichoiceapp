using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel;
using RektaRetailApp.Web.ApiModel.Supplier;
using RektaRetailApp.Web.DomainEvents.Supplier;

namespace RektaRetailApp.Web.Commands.Supplier
{
    public class CreateSupplierCommand : IRequest<Response<SupplierApiModel>>
    {
        public string Name { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        [CanBeNull] public string? Description { get; set; }
    }


    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, Response<SupplierApiModel>>
    {
        private readonly ISupplierRepository _repo;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateSupplierCommandHandler(ISupplierRepository repo, IMediator mediator, IMapper mapper)
        {
            _repo = repo;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<Response<SupplierApiModel>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            await _repo.CreateSupplierAsync(request).ConfigureAwait(false);
            await _repo.SaveAsync().ConfigureAwait(false);
            var supplier = await _repo
                .GetSupplierBy(null,s => s.MobileNumber!.Equals(request.PhoneNumber),
                    s => s.Name!.Equals(request.Name!.Trim().ToUpperInvariant())).ConfigureAwait(false);
            var model = _mapper.Map<SupplierApiModel>(supplier);

            var result = new Response<SupplierApiModel>(model, ResponseStatus.Success);

            await _mediator.Publish(new SupplierCreatedEvent(model), cancellationToken).ConfigureAwait(false);

            return result;
        }
    }
}
