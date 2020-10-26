using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.Abstractions;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.Abstractions;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel.Product;
using RektaRetailApp.Web.Commands.Product;
using RektaRetailApp.Web.Data;

namespace RektaRetailApp.Web.Services
{
    public class ProductRepository : GenericBaseRepository, IProductRepository
    {
        private readonly RektaContext _db;
        private readonly IMapper _mapper;
        private readonly DbSet<Product> _set;
        public ProductRepository(IHttpContextAccessor accessor,
            RektaContext db, IMapper mapper) : base(accessor)
        {
            _db = db;
            _mapper = mapper;
            _set = _db.Products;
        }
        public async Task SaveAsync()
        {
            await Commit<Product>(_db);
        }

        public async Task<IEnumerable<ProductApiModel>> GetAllProductsAsync()
        {
            var result = await _set.AsNoTracking()
                .Include(p => p.ProductCategories)
                .Include(p => p.ProductSupplier)
                .ProjectTo<ProductApiModel>(_mapper.ConfigurationProvider)
                .ToListAsync().ConfigureAwait(false);
            return result;
        }

        public async Task<ProductDetailApiModel> GetProductByIdAsync(int id)
        {
            var result = await _set.AsNoTracking()
                .Where(p => p.Id == id)
                .ProjectTo<ProductDetailApiModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            return result;
        }

        public async Task<ProductApiModel> GetProductByAsync(params Expression<Func<Product, bool>>[] searchTerms)
        {
            IQueryable<Product>? query = null;
            foreach (var term in searchTerms)
            {
                query = _set.AsNoTracking().Where(term);
            }

            var result = await query.ProjectTo<ProductApiModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            return result;
        }

        public async Task<ProductApiModel> CreateProductAsync(CreateProductCommand command)
        {
            var product = _mapper.Map<CreateProductCommand, Product>(command);
            product.Name = product.Name.Trim().ToUpperInvariant();
            //TODO: map categoryapimodels to get ids and add the to product
            //TODO: ensure that category names are unique. Guard against this duplication
            _set.Add(product);
            await SaveAsync().ConfigureAwait(false);
            var result = await GetProductByAsync(p => p.Name.Equals(command.Name.ToUpperInvariant()),
                p => p.RetailPrice == command.RetailPrice, p => p.SuppliedPrice == command.SuppliedPrice,
                p => p.SupplierId == command.SupplierId);
            return result;
        }

        public Task<ProductApiModel> UpdateProductAsync(UpdateProductCommand command)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(DeleteProductCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
