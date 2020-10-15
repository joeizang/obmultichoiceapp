using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.Abstractions;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel.Inventory;
using RektaRetailApp.Web.Data;

namespace RektaRetailApp.Web.Services
{
    public class InventoryRepository : GenericBaseRepository, IInventoryRepository
    {
        private readonly RektaContext _db;
        private readonly IMapper _mapper;
        private readonly DbSet<Inventory> _set;

        public InventoryRepository(IHttpContextAccessor accessor, RektaContext db, IMapper mapper) : base(accessor)
        {
            _db = db;
            _set = _db.Set<Inventory>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryApiModel>> GetAllInventories(string? searchTerm, bool ascending)
        {
            var query = _set.AsNoTracking()
                        .Include(i => i.Category)
                        .Include(i => i.InventoryItem);
            var nonNullSearchTerm = Guard.Against.NullOrEmpty(searchTerm, nameof(searchTerm));
            var newQuery = query.Where(x =>
                x.BatchNumber != null && x.BatchNumber.Equals(searchTerm) && x.Name.Equals(searchTerm));
            IQueryable orderedQuery;
            if (ascending == false)
                orderedQuery = newQuery.OrderByDescending(x => x.SupplyDate).ThenByDescending(x => x.Name)
                    .ThenByDescending(x => x.BatchNumber);
            orderedQuery = newQuery.OrderBy(x => x.SupplyDate).ThenBy(x => x.Name).ThenBy(x => x.BatchNumber);

            var result = await orderedQuery
                .ProjectTo<InventoryApiModel>(_mapper.ConfigurationProvider)
                .ToListAsync().ConfigureAwait(false);
            return result;
        }

        public async Task<InventoryDetailApiModel> GetInventoryById(int id)
        {
            var result = await _db.Inventories.AsNoTracking()
                .Where(i => i.Id == id).ProjectTo<InventoryDetailApiModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync().ConfigureAwait(false);
            return result;
        }

        public Task<IEnumerable<InventoryApiModel>> GetInventoriesBy(Expression<Func<string?, string?, string, Inventory>> searchOptions)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync()
        {
            await Commit<Inventory>(_db);
        }
    }
}
