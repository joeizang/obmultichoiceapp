using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.ApiModel.Product;
using RektaRetailApp.Web.Commands.Product;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.Queries.Product;

namespace RektaRetailApp.Web.Abstractions.Entities
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
