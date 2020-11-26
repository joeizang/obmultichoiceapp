using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MoreLinq.Extensions;
using ObmultichoiceRetailer.Domain.DomainModels;
using ObmultichoiceRetailer.Web.Abstractions;
using ObmultichoiceRetailer.Web.Abstractions.Entities;
using ObmultichoiceRetailer.Web.ApiModel.Sales;
using ObmultichoiceRetailer.Web.Commands.Sales;
using ObmultichoiceRetailer.Web.Data;
using ObmultichoiceRetailer.Web.Helpers;
using ObmultichoiceRetailer.Web.Queries.Sales;

namespace ObmultichoiceRetailer.Web.Services
{
    public class SalesRepository : GenericBaseRepository, ISalesRepository
    {
        private readonly ObmultichoiceContext _db;
        private readonly IMapper _mapper;
        private readonly DbSet<Sale> _set;
        public SalesRepository([NotNull] IHttpContextAccessor accessor,
            [NotNull] ObmultichoiceContext db, IMapper mapper) : base(accessor, db)
        {
            _db = db;
            _mapper = mapper;
            _set = _db.Sales;
        }

        public async Task CreateSale(CreateSaleCommand command, CancellationToken token)
        {
            var products = _db.Products.Include(p => p.Inventory);
            var sale = _mapper.Map<Sale>(command);
            Product? product = null;
            //make deductions from the quantity of every product that has been sold
            foreach(var p in command.ProductsSold)
            {
                product = await products.SingleOrDefaultAsync(x => x.Id == p.Id,token).ConfigureAwait(false);

                product.Quantity -= p.Quantity;
                if (product.Inventory != null) product.Inventory.Quantity -= p.Quantity;
                _db.Entry(product).State = EntityState.Modified;
            }
            //if there are any discounts then you can calculate before persisting.
            sale.ItemsSold.Add(product!);
            _set.Add(sale);
        }

        public Task UpdateSale(UpdateSaleCommand command, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task CancelASale(CancelSaleCommand command, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<SaleApiModel>> GetAllSales(GetAllSalesQuery query, CancellationToken token)
        {
            var queryable = _set.AsNoTracking();

            
            //TODO: BUILD YOUR DIFFERENT QUERIES ON THE IQUERYABLE INSTANCE TO SEARCH FILTER AND ORDER
            if (query.SearchTerm is null && query.OrderByTerm is null)
            {
                    var saleQueryable = queryable.Select(s => new SaleApiModel
                    {
                        Id = s.Id,
                        TypeOfPayment = s.ModeOfPayment,
                        SaleDate = s.SaleDate,
                        SalesPerson = s.SalesPerson,
                        GrandTotal = s.GrandTotal,
                        NumberOfItemsSold = s.ItemsSold.Count,
                        TypeOfSale = s.TypeOfSale,
                        ProductsBought = s.ItemsSold.Select(x => new ItemSoldApiModel
                        {
                            Id = x.Id,
                            ItemName = x.Name,
                            Price = x.RetailPrice,
                            Quantity = x.Quantity,
                            ProductCategory = string.Join(",", x.ProductCategories.Select(x => x.Name).ToArray())
                        }).ToList()
                    });

                    var result = new PagedList<SaleApiModel>(
                        await saleQueryable.CountAsync(token).ConfigureAwait(false), query.PageNumber, query.PageSize,
                        await saleQueryable.ToListAsync(token).ConfigureAwait(false));
                    return result;
            }

            var parsedTotal = decimal.Parse(query.SearchTerm!);
            var parsedDate = DateTime.Parse(query.SearchTerm!);
            queryable = queryable.Where(s => s.SalesPerson.Equals(query.SearchTerm) || s.SaleDate.Equals(parsedDate));
            queryable = queryable.OrderBy(x => x.SaleDate);
            //TODO: ALSO FACTOR IN THAT YOUR RESULT MUST BE PAGINATED.

            var processedResult = await PagedList<SaleApiModel>.CreatePagedList(
                queryable.Select(x => new SaleApiModel
                {
                    GrandTotal = x.GrandTotal,
                    Id = x.Id,
                    NumberOfItemsSold = x.ItemsSold.Count,
                    SaleDate = x.SaleDate,
                    SalesPerson = x.SalesPerson,
                    TypeOfPayment = x.ModeOfPayment,
                    TypeOfSale = x.TypeOfSale
                }), query.PageNumber, query.PageSize, token).ConfigureAwait(false);
            return processedResult;
        }

        public async Task<Sale> GetSaleById(GetSaleByIdQuery query, CancellationToken token)
        {
            return await _set.FindAsync(query.Id, token).ConfigureAwait(false);

        }

        public Task SaveAsync(CancellationToken token)
        {
            return Commit<Sale>(token);
        }
    }
}
