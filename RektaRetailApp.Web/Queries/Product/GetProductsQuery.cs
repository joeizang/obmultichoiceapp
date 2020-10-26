using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel.Product;

namespace RektaRetailApp.Web.Queries.Product
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductApiModel>>
    {

    }


    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery,IEnumerable<ProductApiModel>>
    {
        private readonly IProductRepository _repo;

        public GetProductsQueryHandler(IProductRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<ProductApiModel>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetAllProductsAsync();
            return result;
        }
    }
}
