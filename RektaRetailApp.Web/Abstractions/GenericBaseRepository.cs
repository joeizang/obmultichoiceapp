using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.Abstractions;
using RektaRetailApp.Domain.DomainModels;

namespace RektaRetailApp.Web.Abstractions
{
    public class GenericBaseRepository : IRepository
    {
        private readonly IHttpContextAccessor _accessor;

        public GenericBaseRepository(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public async Task Commit<T>(DbContext db) where T : BaseDomainModel
        {
            var user = _accessor.HttpContext.User.Identity.Name ?? "Anonymous User";
            foreach (var entity in db.ChangeTracker.Entries<T>())
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
                        entity.Entity.UpdatedBy = user;
                    }
                }

            }

            await db.SaveChangesAsync().ConfigureAwait(false);
        }

    }
}
