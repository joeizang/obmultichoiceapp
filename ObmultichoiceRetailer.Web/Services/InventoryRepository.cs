using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ObmultichoiceRetailer.Domain.DomainModels;
using ObmultichoiceRetailer.Web.Abstractions;
using ObmultichoiceRetailer.Web.Abstractions.Entities;
using ObmultichoiceRetailer.Web.ApiModel.Inventory;
using ObmultichoiceRetailer.Web.Commands.Inventory;
using ObmultichoiceRetailer.Web.Data;
using ObmultichoiceRetailer.Web.Helpers;
using ObmultichoiceRetailer.Web.Queries.Inventory;

namespace ObmultichoiceRetailer.Web.Services
{
  public class InventoryRepository : GenericBaseRepository, IInventoryRepository
  {
    private readonly ObmultichoiceContext _db;
    private readonly IMapper _mapper;
    private readonly DbSet<Inventory> _set;

    public InventoryRepository(IHttpContextAccessor accessor, ObmultichoiceContext db, IMapper mapper) : base(accessor,db)
    {
      _db = db;
      _set = _db.Set<Inventory>();
      _mapper = mapper;
    }

    public async Task<PagedList<InventoryApiModel>> GetAllInventories(GetAllInventoriesQuery query, CancellationToken token)
    {
      IQueryable<Inventory> queryable = _set.AsNoTracking()
          .Include(i => i.InventoryItems);

      if (query.SearchString is null && query.OrderBy is null)
      {
          var result = await PagedList<InventoryApiModel>
              .CreatePagedList(queryable.Select(i => new InventoryApiModel
              {
                  Name = i.Name,
                  Id = i.Id,
                  NumberOfProductsInStock = i.Quantity,
                  CategoryName = string.Join(",", i.InventoryItems.Select(p => p.ProductCategories.Select(c => c.Name)))
              }), query.PageNumber, query.PageSize, token)
              .ConfigureAwait(false);
          return result;
      }

      queryable = queryable.Where(i => i.Name.Equals(query.SearchString));

      var result1 = await PagedList<InventoryApiModel>.CreatePagedList(queryable.Select(i => new InventoryApiModel
          {
              Name = i.Name,
              Id = i.Id,
              NumberOfProductsInStock = i.Quantity,
              CategoryName = string.Join(",", i.InventoryItems.Select(p => p.ProductCategories.Select(c => c.Name)))
          }), query.PageNumber, query.PageSize, token)
          .ConfigureAwait(false);
      return result1;
    }

    public async Task<InventoryDetailApiModel> GetInventoryById(int id, CancellationToken cancellationToken)
    {
      var result = await _set.AsNoTracking().Include(i => i.InventoryItems)
          .Where(i => i.Id == id).ProjectTo<InventoryDetailApiModel>(_mapper.ConfigurationProvider)
          .SingleOrDefaultAsync(cancellationToken).ConfigureAwait(false);
      return result;
    }

    public async Task<InventoryApiModel> GetInventoryBy(params Expression<Func<Inventory, bool>>[] searchTerms)
    {
      IQueryable<Inventory>? query = null;
      foreach (var term in searchTerms)
      {
        query = _set.AsNoTracking().Where(term);
      }

      var result = await query
          .ProjectTo<InventoryApiModel>(_mapper.ConfigurationProvider)
          .SingleOrDefaultAsync().ConfigureAwait(false);
      return result;
    }

    public void CreateInventory(CreateInventoryCommand command)
    {
      var inventory = _mapper.Map<CreateInventoryCommand, Inventory>(command);
      inventory.Description = inventory.Description?.ToUpperInvariant().Trim();
      inventory.Name = inventory.Name.Trim().ToUpperInvariant();

      _set.Add(inventory);
    }

    public async Task UpdateInventory(UpdateInventoryCommand command, CancellationToken token)
    {
      var target = await GetInventoryById(command.InventoryId, token).ConfigureAwait(false);
      target.Name = command.Name.Trim().ToUpperInvariant();

      _db.Entry(target).State = EntityState.Modified;
    }

    public async Task DeleteInventory(DeleteInventoryCommand command)
    {
        var result = await _set.FindAsync(command.Id).ConfigureAwait(false);

        _set.Remove(result);
    }

    public async Task SaveAsync(CancellationToken token)
    {
      await Commit<Inventory>(token).ConfigureAwait(false);
    }
  }
}
