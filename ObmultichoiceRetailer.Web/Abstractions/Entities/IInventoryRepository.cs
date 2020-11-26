using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObmultichoiceRetailer.Domain.DomainModels;
using ObmultichoiceRetailer.Web.ApiModel.Inventory;
using ObmultichoiceRetailer.Web.Commands.Inventory;
using ObmultichoiceRetailer.Web.Data;

namespace ObmultichoiceRetailer.Web.Abstractions.Entities
{
  public interface IInventoryRepository : IRepository
  {
    Task<IEnumerable<InventoryApiModel>> GetAllInventories(string? searchTerm, bool ascending = true);

    Task<InventoryDetailApiModel> GetInventoryById(int id);

    Task<Inventory> GetInventoryById(UpdateInventoryCommand id);

    Task<InventoryApiModel> GetInventoryBy(params Expression<Func<Inventory, bool>>[] searchTerms);

    Task<IEnumerable<InventoryApiModel>> GetInventoriesBy(params Expression<Func<Inventory, bool>>[] searchTerms);

    void CreateInventory(CreateInventoryCommand command);

    Task UpdateInventory(UpdateInventoryCommand command);

    Task DeleteInventory(DeleteInventoryCommand command);

    Task SaveAsync(CancellationToken token);
  }
}