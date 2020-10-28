using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel;
using RektaRetailApp.Web.ApiModel.Supplier;
using RektaRetailApp.Web.Helpers;

namespace RektaRetailApp.Web.Queries.Supplier
{
    public class GetAllSuppliersQuery : IRequest<PaginatedResponse<SupplierApiModel>>
    {
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }

        public string UriName { get; set; }
    }

    public class GetAllSuppliersQueryHandler : IRequestHandler<GetAllSuppliersQuery, PaginatedResponse<SupplierApiModel>>
    {
        private readonly IMapper _mapper;
        private readonly ISupplierRepository _repo;
        private readonly LinkGenerator _generator;
        private readonly IHttpContextAccessor _accessor;

        public GetAllSuppliersQueryHandler(IMapper mapper, ISupplierRepository repo, LinkGenerator generator, IHttpContextAccessor accessor)
        {
            _mapper = mapper;
            _repo = repo;
            _generator = generator;
            _accessor = accessor;
        }
        public async Task<PaginatedResponse<SupplierApiModel>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _repo.GetSuppliersAsync()
                .ProjectTo<SupplierApiModel>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber!.Value, request.PageSize!.Value)
                .ConfigureAwait(false);


            var prevLink = pagedResult.HasPrevious ?
                _generator.GetPathByAction(_accessor.HttpContext) : _accessor.HttpContext.Request.GetDisplayUrl();
            var nextLink = pagedResult.HasNext
                ? _generator.GetPathByAction(_accessor.HttpContext) : _accessor.HttpContext.Request.GetDisplayUrl();


            var result = new PaginatedResponse<SupplierApiModel>(pagedResult, 
                new PaginatedMetaData(pagedResult.TotalCount,pagedResult.PageSize,pagedResult.CurrentPage,prevLink,nextLink));
            //_accessor.HttpContext.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize());
            return result;
        }
    }
}
