using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.Abstractions;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel.Supplier;
using RektaRetailApp.Web.Commands.Supplier;
using RektaRetailApp.Web.Data;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.Queries.Supplier;

namespace RektaRetailApp.Web.Services
{
  public class SupplierRepository : GenericBaseRepository, ISupplierRepository
  {
    private readonly RektaContext _db;

    private readonly IMapper _mapper;
    private readonly DbSet<Supplier> _set;

    public SupplierRepository([NotNull] IHttpContextAccessor accessor,
        RektaContext db,
        IMapper mapper) : base(accessor, db)
    {
      _db = db;
      _set = _db.Set<Supplier>();
      _mapper = mapper;
    }

    public Task SaveAsync()
    {
      return Commit<Supplier>();
    }

    public Task<PagedList<SupplierApiModel>> GetSuppliersAsync(GetAllSuppliersQuery query)
    {
      IQueryable<Supplier> suppliers = _set.AsNoTracking();

      if (query.SearchTerm is null)
      {
        if (query.PageSize is null || query.PageNumber is null)
          return suppliers.ProjectTo<SupplierApiModel>(_mapper.ConfigurationProvider)
              .PaginatedListAsync(1, 10);
        var result = suppliers.ProjectTo<SupplierApiModel>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(query.PageNumber.Value, query.PageSize.Value);
        return result;
      }

      suppliers = suppliers.Where(s => s.MobileNumber != null && s.Name != null &&
                                       s.Name.Equals(query.SearchTerm) &&
                                       s.MobileNumber.Equals(query.SearchTerm));
      if (query.PageSize == null && query.PageNumber == null)
        return suppliers.ProjectTo<SupplierApiModel>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(1, 10);
      var supplierResults = suppliers.ProjectTo<SupplierApiModel>(_mapper.ConfigurationProvider)
          .PaginatedListAsync(query.PageNumber!.Value, query.PageSize!.Value);
      return supplierResults;
    }

    public Task<Supplier> GetSupplierById(int id)
    {
      return _set.AsNoTracking().Include(s => s.ProductsSupplied).SingleOrDefaultAsync(s => s.Id == id);
    }

    public Task<Supplier> GetSupplierBy(Expression<Func<Supplier, object>>[]? includes = null,
        params Expression<Func<Supplier, bool>>[] searchTerms)
    {
      var supplier = GetOneBy<Supplier>(includes, searchTerms);
      return supplier;
    }

    public Task CreateSupplierAsync(CreateSupplierCommand command)
    {
      var supplier = _mapper.Map<CreateSupplierCommand, Supplier>(command);
      supplier.Name = supplier.Name!.Trim().ToUpperInvariant();
      supplier.MobileNumber = supplier.MobileNumber!.Trim().ToUpperInvariant();
      supplier.Description = supplier.Description!.Trim().ToUpperInvariant();
      return Task.Run(() => _set.Attach(supplier));
    }

    public void UpdateSupplier(UpdateSupplierCommand command)
    {
      var exists = _set.AsNoTracking().SingleOrDefault(x => x.Id == command.Id);
      if (exists is null) return;
      exists.Name = exists.Name?.Trim().ToUpperInvariant();
      exists.MobileNumber = exists.MobileNumber?.Trim().ToUpperInvariant();
      exists.Description = exists.Description?.Trim().ToUpperInvariant();
      exists.CreatedBy = exists.CreatedBy.Trim().ToUpperInvariant();
      _db.Entry(exists).State = EntityState.Modified;
    }

    public void DeleteSupplier(int id)
    {
        var found = _set.Find(id);
        if (found is null) return;
        found.IsDeleted = true;
        _db.Entry(found).State = EntityState.Modified;
    }


  }
}
