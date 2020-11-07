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
            if (!(query.PageNumber is null) && !(query.PageSize is null) || !(query.SearchTerm is null))
            {
                products = products.Where(p => p.Brand!.Contains(query.SearchTerm!.Trim().ToUpperInvariant())
                                               && p.Name.Contains(query.SearchTerm));
                var paged = products.AsQueryable().ProjectTo<ProductApiModel>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(query.PageNumber!.Value, query.PageSize!.Value);
                return paged;
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
                product.Brand = product.Brand?.Trim().ToUpperInvariant();
                product.Comments = product.Comments?.Trim().ToUpperInvariant();
                _set.Attach(product);
            });
        }

        public Task UpdateProductAsync(UpdateProductCommand command)
        {
            return Task.Run(() =>
            {
                var target = _set.Find(command.Id);
                if (!(target is null))
                {
                    target.Brand = command.Brand?.Trim().ToUpperInvariant();
                    target.Name = command.Name.Trim().ToUpperInvariant();
                    target.ImageUrl = command.ImageUrl;
                    target.Quantity = command.Quantity;
                    target.ReorderPoint = command.ReorderPoint;
                    target.RetailPrice = command.RetailPrice;
                    target.SuppliedPrice = command.SuppliedPrice;
                    target.UnitPrice = command.UnitPrice;
                    target.UnitMeasure = command.UnitMeasure;
                    target.Verified = command.Verified;
                    target.SupplyDate = command.SupplyDate;
                    target.SupplierId = command.SupplierId;

                    _db.Entry(target).State = EntityState.Modified;
                }
                throw new ArgumentException("The Id given doesn't identify any object in persistence!");
            });
        }

        public Task DeleteProductAsync(DeleteProductCommand command)
        {
            return Task.Run(() =>
            {
                var product = _set.Find(command.Id);
                if (product is null)
                    throw new ArgumentException("The id given doesn't belong to a real product!");
                product.IsDeleted = true;
                _db.Entry(product).State = EntityState.Modified;
            });
        }
    }
}
