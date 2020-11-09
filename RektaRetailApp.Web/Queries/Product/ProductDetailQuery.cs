using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel;
using RektaRetailApp.Web.ApiModel.Product;

namespace RektaRetailApp.Web.Queries.Product
{
    public class ProductDetailQuery : IRequest<Response<ProductDetailApiModel>>
    {
        public int Id { get; set; }
    }


    public class ProductDetailQueryHandler : IRequestHandler<ProductDetailQuery, Response<ProductDetailApiModel>>
    {
        private readonly IProductRepository _repo;

        public ProductDetailQueryHandler(IProductRepository repo)
        {
            _repo = repo;
        }
        public async Task<Response<ProductDetailApiModel>> Handle(ProductDetailQuery request, CancellationToken cancellationToken)
        {
            var includes = new Expression<Func<Domain.DomainModels.Product, object>>[]
            {
                p => p.ProductCategories,
                p => p.ProductSupplier
            };
            var product = await _repo.GetProductByAsync(includes, p => p.Id == request.Id)
                .ConfigureAwait(false);
            //var data = _mapper.Map<Domain.DomainModels.Product, ProductDetailApiModel>(product);
            var data = new ProductDetailApiModel(product.RetailPrice,product.UnitPrice,product.Name,product.Quantity,
                product.SuppliedPrice,product.ProductSupplier.Name,product.ProductSupplier.MobileNumber,
                product.ImageUrl,product.SupplyDate);
            var result = new Response<ProductDetailApiModel>(data, ResponseStatus.Success);
            return result;
        }
    }
}
