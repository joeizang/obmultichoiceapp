using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
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

        public SupplierRepository([NotNull] IHttpContextAccessor accessor, 
            RektaContext db,
            IMapper mapper) : base(accessor,db)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task SaveAsync()
        {
            await Commit<Supplier>().ConfigureAwait(false);
        }

        public Task<IEnumerable<SupplierApiModel>> GetSuppliersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SupplierDetailApiModel> GetSupplierById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SupplierApiModel> GetSupplierBy(params Expression<Func<Supplier, bool>>[] searchTerms)
        {
            throw new NotImplementedException();
        }

        public Task CreateSupplierAsync(CreateSupplierCommand command)
        {
            var supplier = _mapper.Map<CreateSupplierCommand, Supplier>(command);

        }
    }
}
