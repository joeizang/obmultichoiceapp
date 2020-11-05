using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel;
using RektaRetailApp.Web.ApiModel.Product;
using RektaRetailApp.Web.Helpers;

namespace RektaRetailApp.Web.Queries.Product
{
    public class GetAllProductsQuery : IRequest<PaginatedResponse<ProductApiModel>>
    {
        public string? SearchTerm { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }


    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginatedResponse<ProductApiModel>>
    {
        private readonly IProductRepository _repo;
        private readonly LinkGenerator _generator;
        private readonly IHttpContextAccessor _accessor;

        public GetAllProductsQueryHandler(IProductRepository repo, LinkGenerator generator, IHttpContextAccessor accessor)
        {
            _repo = repo;
            _generator = generator;
            _accessor = accessor;
        }
        public async Task<PaginatedResponse<ProductApiModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            //TODO: deal with issues arising from using automapper not mapping properties correctly.
            var products = await _repo.GetAllProducts(request);


            var prev = products.HasPrevious ?
                _generator.GetPathByName(_accessor.HttpContext, "GetAllProducts",
                    new { pageNumber = request.PageNumber - 1, pageSize = request.PageSize, searchTerm = request.SearchTerm })
                : null;
            var next = products.HasNext ?
                _generator.GetPathByName(_accessor.HttpContext, "GetAllProducts",
                    new { pageNumber = request.PageNumber + 1, pageSize = request.PageSize, searchTerm = request.SearchTerm })
                : null;

            var result = new PaginatedResponse<ProductApiModel>(products,
                products.TotalCount, products.PageSize, products.CurrentPage, prev, next);
            return result;
        }
    }
}
