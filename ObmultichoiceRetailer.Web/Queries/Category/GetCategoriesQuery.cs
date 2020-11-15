using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ObmultichoiceRetailer.Web.Abstractions;
using ObmultichoiceRetailer.Web.Abstractions.Entities;
using ObmultichoiceRetailer.Web.ApiModel.Category;

namespace ObmultichoiceRetailer.Web.Queries.Category
{
    public class GetCategoriesQuery : IRequest<IEnumerable<CategoryApiModel>>
    {
    }

    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryApiModel>>
    {
        private readonly ICategoryRepository _repo;

        public GetCategoriesQueryHandler(ICategoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<CategoryApiModel>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetCategories().ConfigureAwait(false);
            return result;
        }
    }
}
