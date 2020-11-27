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
using ObmultichoiceRetailer.Web.Helpers;
using ObmultichoiceRetailer.Web.Queries.Inventory;

namespace ObmultichoiceRetailer.Web.Abstractions.Entities
{
  public interface IInventoryRepository : IRepository
  {
    Task<PagedList<InventoryApiModel>> GetAllInventories(GetAllInventoriesQuery query, CancellationToken token);

    Task<InventoryDetailApiModel> GetInventoryById(int id, CancellationToken cancellationToken);

    Task<InventoryApiModel> GetInventoryBy(params Expression<Func<Inventory, bool>>[] searchTerms);

    void CreateInventory(CreateInventoryCommand command);

    Task UpdateInventory(UpdateInventoryCommand command, CancellationToken token);

    Task DeleteInventory(DeleteInventoryCommand command);

    Task SaveAsync(CancellationToken token);
  }
}