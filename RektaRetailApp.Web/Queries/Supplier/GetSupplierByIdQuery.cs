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

namespace RektaRetailApp.Web.Queries.Supplier
{
    public class GetSupplierByIdQuery : IRequest<Response<SupplierDetailApiModel>>
    {
        public int Id { get; set; }
    }


    public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, Response<SupplierDetailApiModel>>
    {
        private readonly ISupplierRepository _repo;
        private readonly IMapper _mapper;

        public GetSupplierByIdQueryHandler(ISupplierRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<Response<SupplierDetailApiModel>> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var supplier = await _repo.GetSupplierById(request.Id);
            var model = _mapper.Map<SupplierDetailApiModel>(supplier);
            var result = new Response<SupplierDetailApiModel>(model, ResponseStatus.Success);
            return result;
        }
    }
}
