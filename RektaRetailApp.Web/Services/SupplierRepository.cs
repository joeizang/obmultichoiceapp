using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.Abstractions;
using RektaRetailApp.Web.Abstractions.Entities;
using RektaRetailApp.Web.ApiModel.Supplier;
using RektaRetailApp.Web.Commands.Supplier;
using RektaRetailApp.Web.Data;

namespace RektaRetailApp.Web.Services
{
    public class SupplierRepository : GenericBaseRepository, ISupplierRepository
    {
        private readonly RektaContext _db;

        private readonly IMapper _mapper;
        private readonly DbSet<Supplier> _set;

        public SupplierRepository([NotNull] IHttpContextAccessor accessor, 
            RektaContext db,
            IMapper mapper) : base(accessor,db)
        {
            _db = db;
            _set = _db.Set<Supplier>();
            _mapper = mapper;
        }

        public Task SaveAsync()
        {
            return Commit<Supplier>();
        }

        public IQueryable<Supplier> GetSuppliersAsync()
        {
            return _set.AsNoTracking();
        }

        public Task<Supplier> GetSupplierById(int id)
        {
            return _set.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);
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
            return Task.Run(() => _set.Add(supplier));
        }
    }
}
