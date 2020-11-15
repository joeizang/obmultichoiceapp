﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ObmultichoiceRetailer.Domain.Abstractions;
using ObmultichoiceRetailer.Domain.DomainModels;
using ObmultichoiceRetailer.Web.Data;

namespace ObmultichoiceRetailer.Web.Abstractions
{
  public class GenericBaseRepository : IRepository
  {
    private readonly IHttpContextAccessor _accessor;
    private readonly ObmultichoiceContext _db;

    public GenericBaseRepository(IHttpContextAccessor accessor, ObmultichoiceContext db)
    {
      _accessor = accessor;
      _db = db;
    }
    public async Task Commit<T>() where T : BaseDomainModel
    {
      var user = _accessor.HttpContext?.User.Identity?.Name ?? "Anonymous User";
      foreach (var entity in _db.ChangeTracker.Entries<T>())
      {
        if (entity.State == EntityState.Added)
        {
          entity.Entity.CreatedAt = DateTimeOffset.Now.LocalDateTime;
          entity.Entity.UpdatedAt = DateTimeOffset.Now.LocalDateTime;
          if (string.IsNullOrEmpty(entity.Entity.CreatedBy))
          {
            entity.Entity.CreatedBy = user;
            entity.Entity.UpdatedBy = user;
          }
        }

        if (entity.State == EntityState.Modified)
        {
          entity.Entity.UpdatedAt = DateTimeOffset.Now.LocalDateTime;
          if (string.IsNullOrEmpty(entity.Entity.UpdatedBy))
          {
            entity.Entity.UpdatedBy = user.ToUpperInvariant();
          }
        }

      }

      await _db.SaveChangesAsync().ConfigureAwait(false);
    }

    public Task<T> GetOneBy<T>(Expression<Func<T, object>>[]? includes = null,
        params Expression<Func<T, bool>>[] searchTerms) where T : BaseDomainModel
    {
      var query = _db.Set<T>().AsNoTracking();

      if (includes is null || includes.Length == 0)
      {
        query = searchTerms.Aggregate(query, (current, term) => current.Where(term));
        var result = query.SingleOrDefaultAsync();
        return result;
      }

      query = includes.Aggregate(query, (current, include) => current.Include(include));
      query = searchTerms.Aggregate(query, (current, term) => current.Where(term));
      var altResult = query.SingleOrDefaultAsync();
      return altResult;
    }
  }
}
