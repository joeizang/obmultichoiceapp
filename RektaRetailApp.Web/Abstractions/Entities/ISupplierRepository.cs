using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.ApiModel.Supplier;
using RektaRetailApp.Web.Commands.Supplier;
using RektaRetailApp.Web.Helpers;
using RektaRetailApp.Web.Queries.Supplier;

namespace RektaRetailApp.Web.Abstractions.Entities
{
    public interface ISupplierRepository : IRepository
    {
        Task SaveAsync();

        Task<PagedList<SupplierApiModel>> GetSuppliersAsync(GetAllSuppliersQuery query);

        Task<Supplier> GetSupplierById(int id);

        Task<Supplier> GetSupplierBy(Expression<Func<Supplier, object>>[]? includes = null, params Expression<Func<Supplier, bool>>[] searchTerms);

        Task CreateSupplierAsync(CreateSupplierCommand command);

        void UpdateSupplier(UpdateSupplierCommand command);
        void DeleteSupplier(int id);
    }
}
