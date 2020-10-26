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
    public class ProductDetailQuery : IRequest<ProductDetailApiModel>
    {
        public int Id { get; set; }
    }


    public class ProductDetailQueryHandler : IRequestHandler<ProductDetailQuery, ProductDetailApiModel>
    {
        private readonly IProductRepository _repo;

        public ProductDetailQueryHandler(IProductRepository repo)
        {
            _repo = repo;
        }
        public async Task<ProductDetailApiModel> Handle(ProductDetailQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetProductByIdAsync(request.Id)
                .ConfigureAwait(false);
            return result;
        }
    }
}
