using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.ApiModel.Product;
using RektaRetailApp.Web.Commands.Product;

namespace RektaRetailApp.Web.Abstractions.Entities
{
    public interface IProductRepository : IRepository
    {
        Task<IEnumerable<ProductApiModel>> GetAllProductsAsync();

        Task<ProductDetailApiModel> GetProductByIdAsync(int id);

        Task<ProductApiModel> GetProductByAsync(params Expression<Func<Product, bool>>[] searchTerms);

        Task<ProductApiModel> CreateProductAsync(CreateProductCommand command);

        Task<ProductApiModel> UpdateProductAsync(UpdateProductCommand command);

        Task DeleteProductAsync(DeleteProductCommand command);

        Task SaveAsync();
    }
}
