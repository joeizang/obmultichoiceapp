using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ObmultichoiceRetailer.Web.Abstractions.Entities;
using ObmultichoiceRetailer.Web.ApiModel.Category;

namespace ObmultichoiceRetailer.Web.Commands.Category
{
    public class DeleteCategoryCommand : IRequest
    {
        public int Id { get; set; }
    }


    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _repo;

        public DeleteCategoryCommandHandler(ICategoryRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await _repo.Delete(new DeleteCategoryApiModel { Id = request.Id }).ConfigureAwait(false);
            await _repo.SaveAsync(cancellationToken).ConfigureAwait(false);
            return default;
        }
    }
}
