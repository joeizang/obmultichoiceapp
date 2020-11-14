using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using MediatR;
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

        public string? Uri { get; set; }

        public string? OrderBy { get; set; }
    }


    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginatedResponse<ProductApiModel>>
    {
        private readonly IProductRepository _repo;
        private readonly IUriGenerator _uriGen;

        public GetAllProductsQueryHandler(IProductRepository repo, IUriGenerator uriGen)
        {
            _repo = repo;
            _uriGen = uriGen;
        }
        public async Task<PaginatedResponse<ProductApiModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repo.GetAllProducts(request);

            var prev = _uriGen.AddQueryStringParams("pageNumber", (request.PageNumber - 1).ToString()!);
            prev.AddQueryStringParams("pageSize", request.PageSize.ToString()!);
            var nextL = _uriGen.AddQueryStringParams("pageNumber", (request.PageNumber + 1).ToString()!);
            nextL.AddQueryStringParams("pageSize", request.PageSize.ToString()!);

            var prevLink = products.HasPrevious
                ? prev.GenerateUri() : null;
            var nextLink = products.HasNext
                ? nextL.GenerateUri() : null;

            var result = new PaginatedResponse<ProductApiModel>(products,
                products.TotalCount, products.PageSize, products.CurrentPage, 
                prevLink?.PathAndQuery, nextLink?.PathAndQuery);
            return result;
        }
    }
}
