using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ObmultichoiceRetailer.Domain.DomainModels;
using ObmultichoiceRetailer.Web.ApiModel.Sales;
using ObmultichoiceRetailer.Web.Commands.Sales;
using ObmultichoiceRetailer.Web.Helpers;
using ObmultichoiceRetailer.Web.Queries.Sales;

namespace ObmultichoiceRetailer.Web.Abstractions.Entities
{
    public interface ISalesRepository : IRepository
    {
        Task CreateSale(CreateSaleCommand command, CancellationToken token);

        Task UpdateSale(UpdateSaleCommand command, CancellationToken token);

        Task CancelASale(CancelSaleCommand command, CancellationToken token);

        Task<PagedList<SaleApiModel>> GetAllSales(GetAllSalesQuery query, CancellationToken token);

        Task<Sale> GetSaleById(GetSaleByIdQuery query, CancellationToken token);

        Task SaveAsync(CancellationToken token);
    }
}
