using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.ApiModel.Inventory;
using RektaRetailApp.Web.Data;

namespace RektaRetailApp.Web.Abstractions.Entities
{
    public interface IInventoryRepository : IRepository
    {
        Task<IEnumerable<InventoryApiModel>> GetAllInventories(string? searchTerm, bool ascending = true);

        Task<InventoryDetailApiModel> GetInventoryById(int id);

        Task<IEnumerable<InventoryApiModel>> GetInventoriesBy(
            Expression<Func<string?, string?, string, Inventory>> searchOptions);

        Task SaveAsync();
    }
}