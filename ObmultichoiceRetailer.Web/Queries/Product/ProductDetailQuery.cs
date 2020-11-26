using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ObmultichoiceRetailer.Web.Abstractions.Entities;
using ObmultichoiceRetailer.Web.ApiModel;
using ObmultichoiceRetailer.Web.ApiModel.Product;

namespace ObmultichoiceRetailer.Web.Queries.Product
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
            };
            var product = await _repo.GetProductByAsync(cancellationToken, includes, p => p.Id == request.Id)
                .ConfigureAwait(false);
            //var data = _mapper.Map<Domain.DomainModels.Product, ProductDetailApiModel>(product);
            var data = new ProductDetailApiModel(product.RetailPrice,product.Name,product.Quantity,
                product.CostPrice,product.SupplyDate, product.Id);
            var result = new Response<ProductDetailApiModel>(data, ResponseStatus.Success);
            return result;
        }
    }
}
