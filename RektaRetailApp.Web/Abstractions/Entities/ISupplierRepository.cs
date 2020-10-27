using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RektaRetailApp.Domain.DomainModels;
using RektaRetailApp.Web.ApiModel.Supplier;
using RektaRetailApp.Web.Commands.Supplier;

namespace RektaRetailApp.Web.Abstractions.Entities
{
    public interface ISupplierRepository : IRepository
    {
        Task SaveAsync();

        Task<IEnumerable<SupplierApiModel>> GetSuppliersAsync();

        Task<SupplierDetailApiModel> GetSupplierById(int id);

        Task<Supplier> GetSupplierBy(Expression<Func<Supplier, object>>[]? includes = null, params Expression<Func<Supplier, bool>>[] searchTerms);

        Task CreateSupplierAsync(CreateSupplierCommand command);
    }
}
