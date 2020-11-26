using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ObmultichoiceRetailer.Domain.DomainModels;
using ObmultichoiceRetailer.Web.ApiModel.Product;
using ObmultichoiceRetailer.Web.Commands.Product;
using ObmultichoiceRetailer.Web.Helpers;
using ObmultichoiceRetailer.Web.Queries.Product;

namespace ObmultichoiceRetailer.Web.Abstractions.Entities
{
    public interface IProductRepository : IRepository
    {
        Task<PagedList<ProductApiModel>> GetAllProducts(GetAllProductsQuery query, CancellationToken token);

        Task<Product> GetProductByIdAsync(int id, CancellationToken token);

        Task<Product> GetProductByAsync(CancellationToken token, Expression<Func<Product, object>>[]? includes,
            params Expression<Func<Product, bool>>[] searchTerms);

        Task CreateProductAsync(CreateProductCommand command, CancellationToken token);

        Task UpdateProductAsync(UpdateProductCommand command, CancellationToken token);

        void DeleteProductAsync(DeleteProductCommand command);

        Task SaveAsync(CancellationToken token);
    }
}
