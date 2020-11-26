using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
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

    public async Task<IEnumerable<InventoryApiModel>> GetAllInventories(string? searchTerm, bool ascending)
    {
      IQueryable<Inventory> orderedQuery;
      var query = _set.AsNoTracking()
          .Include(i => i.InventoryItems);
      if (!string.IsNullOrEmpty(searchTerm))
      {
        IQueryable<Inventory> newQuery = query.Where(x =>
            x.Name!.Equals(searchTerm));
        if (ascending == false)
            orderedQuery = newQuery.OrderByDescending(x => x.SupplyDate).ThenByDescending(x => x.Name);
      }

      if (ascending == false)
        orderedQuery = query.OrderByDescending(x => x.SupplyDate).ThenByDescending(x => x.Name);
      orderedQuery = query.OrderBy(x => x.SupplyDate).ThenBy(x => x.Name);

      var result = await orderedQuery
          .ProjectTo<InventoryApiModel>(_mapper.ConfigurationProvider)
          .ToListAsync().ConfigureAwait(false);
      return result;
    }

    public async Task<InventoryDetailApiModel> GetInventoryById(int id)
    {
      var result = await _set.AsNoTracking()
          .Where(i => i.Id == id).ProjectTo<InventoryDetailApiModel>(_mapper.ConfigurationProvider)
          .SingleOrDefaultAsync().ConfigureAwait(false);
      return result;
    }

    public async Task<Inventory> GetInventoryById(UpdateInventoryCommand command)
    {
      var result = await _set.AsNoTracking()
          .Where(i => i.Id == command.InventoryId)
          .SingleOrDefaultAsync().ConfigureAwait(false);
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

    public async Task<IEnumerable<InventoryApiModel>> GetInventoriesBy(params Expression<Func<Inventory, bool>>[] searchTerms)
    {
      IQueryable<Inventory>? query = null;
      foreach (var term in searchTerms)
      {
        query = _set.AsNoTracking().Where(term);
      }

      var result = await query
          .ProjectTo<InventoryApiModel>(_mapper.ConfigurationProvider)
          .ToListAsync().ConfigureAwait(false);
      return result;
    }

    public void CreateInventory(CreateInventoryCommand command)
    {
      var inventory = _mapper.Map<CreateInventoryCommand, Inventory>(command);
      inventory.Description = inventory.Description?.ToUpperInvariant().Trim();
      inventory.Name = inventory.Name.Trim().ToUpperInvariant();

      _set.Add(inventory);
    }

    public async Task UpdateInventory(UpdateInventoryCommand command)
    {
      var target = await GetInventoryById(command);
      target.Name = command.Name.Trim().ToUpperInvariant();
      target.Description = command.Description.Trim().ToUpperInvariant();

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
