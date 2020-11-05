using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.Abstractions;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel.Product;
using RektaRetailApp.Web.Commands.Product;
using RektaRetailApp.Web.Data;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.Queries.Product;

namespace RektaRetailApp.Web.Services
{
    public class ProductRepository : GenericBaseRepository, IProductRepository
    {
        private readonly RektaContext _db;
        private readonly IMapper _mapper;
        private readonly DbSet<Product> _set;
        public ProductRepository(IHttpContextAccessor accessor,
            RektaContext db, IMapper mapper) : base(accessor,db)
        {
            _db = db;
            _mapper = mapper;
            _set = _db.Products;
        }
        public async Task SaveAsync()
        {
            await Commit<Product>().ConfigureAwait(false);
        }

        public Task<PagedList<ProductApiModel>> GetAllProducts(GetAllProductsQuery query)
        {
            IQueryable<Product> products = _set.AsNoTracking();
            if (!(query.PageNumber is null) && !(query.PageSize is null))
            {
                return products.AsQueryable().ProjectTo<ProductApiModel>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(query.PageNumber.Value, query.PageSize.Value);
            }

            var pagedProducts = products.ProjectTo<ProductApiModel>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(1, 10);
            return pagedProducts;
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            var result = _set.AsNoTracking()
                .Where(p => p.Id == id)
                .SingleOrDefaultAsync();
            return result;
        }

        public Task<Product> GetProductByAsync(Expression<Func<Product, object>>[]? includes = null, params Expression<Func<Product, bool>>[] searchTerms)
        {
            var product = GetOneBy(includes, searchTerms);
            return product;
        }

        public Task CreateProductAsync(CreateProductCommand command)
        {
            return Task.Run(() =>
            {
                var product = _mapper.Map<CreateProductCommand, Product>(command);
                product.Name = product.Name.Trim().ToUpperInvariant();
                _set.Attach(product);
            });
        }

        public Task UpdateProductAsync(UpdateProductCommand command)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(DeleteProductCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
