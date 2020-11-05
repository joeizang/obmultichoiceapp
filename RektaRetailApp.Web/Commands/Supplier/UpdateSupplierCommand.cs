using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel;
using RektaRetailApp.Web.ApiModel.Supplier;
using RektaRetailApp.Web.DomainEvents.Supplier;

namespace RektaRetailApp.Web.Commands.Supplier
{
    public class UpdateSupplierCommand : IRequest<Response<SupplierDetailApiModel>>
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? MobileNumber { get; set; }

        public string? Description { get; set; }
    }


    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, Response<SupplierDetailApiModel>>
    {
        private readonly ISupplierRepository _repo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateSupplierCommandHandler(ISupplierRepository repo,IMapper mapper, IMediator mediator)
        {
            _repo = repo;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<Response<SupplierDetailApiModel>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            _repo.UpdateSupplier(request);
            await _repo.SaveAsync();
            var temp = await _repo.GetSupplierById(request.Id);
            var target = _mapper.Map<SupplierDetailApiModel>(temp);
            var supplierUpdated = new Response<SupplierDetailApiModel>(target, ResponseStatus.Success);
            await _mediator.Publish(new SupplierUpdatedEvent(request.Name, request.MobileNumber, request.Description),cancellationToken);
            return supplierUpdated;
        }
    }
}
