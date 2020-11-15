using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        Task<PagedList<ProductApiModel>> GetAllProducts(GetAllProductsQuery query);

        Task<Product> GetProductByIdAsync(int id);

        Task<Product> GetProductByAsync(Expression<Func<Product, object>>[]? includes,
            params Expression<Func<Product, bool>>[] searchTerms);

        Task CreateProductAsync(CreateProductCommand command);

        Task UpdateProductAsync(UpdateProductCommand command);

        Task DeleteProductAsync(DeleteProductCommand command);

        Task SaveAsync();
    }
}
