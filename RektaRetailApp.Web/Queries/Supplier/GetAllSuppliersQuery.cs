using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel.Supplier;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Domain.DomainModels;

namespace RektaRetailApp.Web.Queries.Supplier
{
    public class GetAllSuppliersQuery : IRequest<PagedList<SupplierApiModel>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class GetAllSuppliersQueryHandler : IRequestHandler<GetAllSuppliersQuery, PagedList<SupplierApiModel>>
    {
        private readonly IMapper _mapper;
        private readonly ISupplierRepository _repo;

        public GetAllSuppliersQueryHandler(IMapper mapper, ISupplierRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<PagedList<SupplierApiModel>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetSuppliersAsync()
                .ProjectTo<SupplierApiModel>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize)
                .ConfigureAwait(false);
            return result;
        }
    }
}
